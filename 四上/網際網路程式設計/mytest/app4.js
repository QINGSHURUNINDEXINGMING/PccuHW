var express = require("express");
var http = require("http");
var app = express();
var logger = require("morgan");

app.use(logger("short"));

var morgan = require('morgan');
var fs = require('fs');
var path = require('path');

var accessLogStream = fs.createWriteStream(
  path.join(__dirname, 'access.log'), {flags: 'a'});

app.use(morgan('short', {stream: accessLogStream}));

http.createServer(app).listen(3000);