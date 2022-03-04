const express = require('express')
const {launch, getStream} = require('puppeteer-stream')

const app = express()
const port = 3000
const delay = time => new Promise(res => setTimeout(res, time))
const runAfter = (func, timeout) => new Promise((resolve, reject) => setTimeout(() => func(resolve), timeout))
const runWithTimeout = (func, timeout = 0) => new Promise((resolve, reject) => {
    func(resolve)
    setTimeout(() => reject('Request is taking too long to response'), timeout)
})

app.listen(port, async () => {
    console.log(`Capturing Server is listening on port ${port}`)
})

app.get('/', async (request, response) => {
    const link = request.query.link
    console.log(link)
    const browser = await launch({
        args: ['--autoplay-policy=no-user-gesture-required'],
        headless: false,
        defaultViewport: {
            width: 1920,
            height: 1080
        }
    })
    const page = await browser.newPage()
    await page.goto(link)
    await page.setViewport({width: 800, height: 600})

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
    }, 1000 * 10)

    response.end()
})
