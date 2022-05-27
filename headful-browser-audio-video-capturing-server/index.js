const express = require('express')
const stream = require('stream');
const path = require('path');
const puppeteer = require('puppeteer');
const fs = require('fs')

function str2ab(str) {
    // Convert a UTF-8 String to an ArrayBuffer

    var buf = new ArrayBuffer(str.length); // 1 byte for each char
    var bufView = new Uint8Array(buf);

    for (var i = 0, strLen = str.length; i < strLen; i++) {
        bufView[i] = str.charCodeAt(i);
    }
    return buf;
}

fs.readFile('C:\\dev\\tasks\\2789167\\data_corrupted_3.txt', 'utf8' , (err, data) => {
    if (err) {
        console.error(err)
        return
    }
    fs.appendFileSync('C:\\dev\\tasks\\2789167\\data_corrupted_3.txt_node_js.webm', Buffer.from(str2ab(data)));
})
