const express = require('express')
const {launch, getStream} = require('puppeteer-stream')

const app = express()
const port = 3000
const runAfter = (func, timeout) => new Promise((resolve, reject) => setTimeout(() => func(resolve), timeout))

app.use(express.json());

app.listen(port, 'localhost', async () => {
    console.log(`Capturing Server is listening on port ${port}`)
})

app.post('/', async (request, response) => {
    const {urlOfWebPageToCapture, webPageWidth, webPageHeight} = request.body;
    console.log(urlOfWebPageToCapture)
    const browser = await launch({
        args: ['--autoplay-policy=no-user-gesture-required'],
        headless: false,
        product: 'chrome'
    })
    const page = await browser.newPage()
    await page.goto(urlOfWebPageToCapture)
    await page.setViewport({width: webPageWidth, height: webPageHeight})

    const stream = await getStream(page, {audio: true, video: true})
    stream.pipe(response)

    await runAfter(async done => {
        try {
            await stream.destroy()
            await page.close()
            await browser.close()
            console.log("finished")
        } catch (error) {
        }
        done()
    }, 1000 * 12)

    response.end()
})
