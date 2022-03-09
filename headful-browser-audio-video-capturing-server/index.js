const express = require('express')
const {launch, getStream} = require('puppeteer-stream')

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
    const {urlOfWebPageToCapture, webPageWidth, webPageHeight, durationInSeconds} = request.body
    console.log(`Received a request to capture ${urlOfWebPageToCapture} Web page`)
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

    const stream = await getStream(page, {audio: true, video: true})
    stream.pipe(response)

    await runAfter(async done => {
        try {
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
