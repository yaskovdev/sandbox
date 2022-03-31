const express = require('express')
const stream = require('stream');
const path = require('path');
const puppeteer = require('puppeteer');

class Stream extends stream.Readable {
    constructor(page, options) {
        super(options);
        this.page = page;
    }

    _read() {
    }

    async destroy() {
        super.destroy();
        await this.page.browser().videoCaptureExtension.evaluate(
            (index) => {
                STOP_RECORDING(index);
            },
            this.page._id
        );
    }
}

async function launch(opts) {
    if (!opts) opts = {};

    if (!opts.args) opts.args = [];

    const extensionPath = path.join(__dirname, "extension");
    const extensionId = "jjndjgheafjngoipoacpjgeicjeomjli";
    let loadExtension = false;
    let loadExtensionExcept = false;
    let whitelisted = false;

    opts.args = opts.args.map((x) => {
        if (x.includes("--load-extension=")) {
            loadExtension = true;
            return x + "," + extensionPath;
        } else if (x.includes("--disable-extensions-except=")) {
            loadExtensionExcept = true;
            return "--disable-extensions-except=" + extensionPath + "," + x.split("=")[1];
        } else if (x.includes("--whitelisted-extension-id")) {
            whitelisted = true;
            return x + "," + extensionId;
        }

        return x;
    });

    if (!loadExtension) opts.args.push("--load-extension=" + extensionPath);
    if (!loadExtensionExcept) opts.args.push("--disable-extensions-except=" + extensionPath);
    if (!whitelisted) opts.args.push("--whitelisted-extension-id=" + extensionId);
    if (opts.defaultViewport?.width && opts.defaultViewport?.height)
        opts.args.push(`--window-size=${opts.defaultViewport?.width}x${opts.defaultViewport?.height}`);

    opts.headless = false;

    const browser = await puppeteer.launch(opts);
    browser.encoders = new Map();

    const extensionTarget = await browser.waitForTarget(
        (target) => target.type() === "background_page" && target._targetInfo.title === "Video Capture"
    );

    browser.videoCaptureExtension = await extensionTarget.page();

    await browser.videoCaptureExtension.exposeFunction("sendData", (opts) => {
        const data = Buffer.from(str2ab(opts.data));
        browser.encoders.get(opts.id).push(data);
    });

    return browser;
}

async function getStream(page, opts) {
    const encoder = new Stream(page);
    if (!opts.audio && !opts.video) throw new Error("At least audio or video must be true");
    if (!opts.mimeType) {
        if (opts.video) opts.mimeType = "video/webm";
        else if (opts.audio) opts.mimeType = "audio/webm";
    }
    if (!opts.frameSize) opts.frameSize = 20;

    await page.bringToFront();

    await page.browser().videoCaptureExtension.evaluate(
        (settings) => {
            START_RECORDING(settings)
        },
        {...opts, index: page._id}
    );

    page.browser().encoders.set(page._id, encoder);

    return encoder;
}

function str2ab(str) {
    // Convert a UTF-8 String to an ArrayBuffer

    var buf = new ArrayBuffer(str.length); // 1 byte for each char
    var bufView = new Uint8Array(buf);

    for (var i = 0, strLen = str.length; i < strLen; i++) {
        bufView[i] = str.charCodeAt(i);
    }
    return buf;
}

const HOST = '0.0.0.0'
const PORT = 8080

const runAfter = (func, timeout) => new Promise((resolve) => setTimeout(() => func(resolve), timeout))

const app = express()

app.use(express.json())

app.listen(PORT, HOST, async () => {
    console.log(`Capturing Server is listening on ${HOST}:${PORT}`)
})

const allowRunningChromeAsRoot = '--no-sandbox'
const allowAudioAutoplayInChrome = '--autoplay-policy=no-user-gesture-required'
const product = 'chrome'

app.get('/status', async (request, response) => {
    response.send('RUNNING')
})

app.post('/captures', async (request, response) => {
    const {urlOfWebPageToCapture, webPageWidth, webPageHeight, mimeType, frameRate, durationInSeconds} = request.body
    console.log(`Received the capture request`, request.body)
    const browser = await launch({
        args: [allowRunningChromeAsRoot, allowAudioAutoplayInChrome],
        headless: false,
        product
    })
    console.log(`Successfully launched the ${product} browser`)
    const pages = await browser.pages()
    const page = pages[0]
    await page.goto(urlOfWebPageToCapture)
    await page.setViewport({width: webPageWidth, height: webPageHeight})

    const stream = await getStream(page, {
        audio: true,
        video: true,
        mimeType,
        videoConstraints: {
            mandatory: {
                minWidth: webPageWidth,
                minHeight: webPageHeight,
                maxWidth: webPageWidth,
                maxHeight: webPageHeight,
                minFrameRate: frameRate,
                maxFrameRate: frameRate
            }
        }
    })
    stream.pipe(response)

    await runAfter(async done => {
        try {
            console.log("Going to finish capturing")
            await stream.destroy()
            await page.close()
            await browser.close()
            console.log("Finished capturing")
        } catch (error) {
        }
        done()
    }, 1000 * durationInSeconds)
    response.end()
})
