/*
 * Licensed to the Apache Software Foundation (ASF) under one
 * or more contributor license agreements.  See the NOTICE file
 * distributed with this work for additional information
 * regarding copyright ownership.  The ASF licenses this file
 * to you under the Apache License, Version 2.0 (the
 * "License"); you may not use this file except in compliance
 * with the License.  You may obtain a copy of the License at
 *
 * http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing,
 * software distributed under the License is distributed on an
 * "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY
 * KIND, either express or implied.  See the License for the
 * specific language governing permissions and limitations
 * under the License.
 */

console.log(this);
var app = {
    trailerData: "",

    // Application Constructor
    initialize: function () {
        document.addEventListener('deviceready', this.onDeviceReady.bind(this), false);
    },
    // deviceready Event Handler
    //
    // Bind any cordova events here. Common events are:
    // 'pause', 'resume', etc.
    insertStockName: function () {
        app.trailerData.forEach((item) => {
            Object.keys(item).forEach((key) => {
                if (key === "拖吊責任區" && item[key] !== "") $('<option/>', { 'value': item[key], 'text': item[key] }).appendTo('#area')
            });
        });
        $("#area").selectmenu("refresh", true);
    },
    onDeviceReady: function () {
        console.log(this);//app
        var appthis = this;
        this.checkConnection();
        var url = "https://opendata.cwb.gov.tw/api/v1/rest/datastore/F-D0047-063?Authorization=rdec-key-123-45678-011121314";

        var trailerUrl = "https://data.taipei/opendata/datalist/apiAccess?scope=resourceAquire&rid=24045907-b7c3-4351-b0b8-b93a54b55367";

        $.getJSON(url, function (response) {
            appthis.weatherData = response.records.locations[0].location[2].weatherElement;
            appthis.updateWeather();
            console.log(response);//api
            console.log(appthis.weatherData);
        })
        $.getJSON(trailerUrl, function (response) {
            app.trailerData = response.result.results;
            app.updateaApi();
            console.log(response);//api
            console.log(app.trailerData);
            // console.log(app.trailerData[2]['拖吊責任區']);
            app.insertStockName();
        })

    },

    checkConnection: function () {
        var networkState = navigator.connection.type;
        if (networkState === Connection.NONE) {
            alert("沒有網路連線...");
            navigator.app.exitApp(); // 離開應用程式
        }
    },

    weatherData: "",

    updateWeather: function () {
        //console.log(this);//app

        var minTArray = this.weatherData[8].time;
        var maxTArray = this.weatherData[12].time;
        for (var i = 0; i < minTArray.length; i++) {
            var startTime = this.weatherData[8].time[i].startTime;
            var endTime = this.weatherData[8].time[i].endTime;
            var li = $("<li>");
            li.append($("<h1>").text(this.processTime(startTime, endTime)));
            var minT = this.weatherData[8].time[i].elementValue[0].value;
            var maxT = this.weatherData[12].time[i].elementValue[0].value
            $("<span>").addClass("ui-li-count").text(minT + " ~ " + maxT).appendTo(li);


            $("#weatherList").append(li);
        }
        $("#weatherList").listview("refresh");
    },
    processTime: function (startstr, endstr) {
        var idx1 = startstr.indexOf('-');
        var idx2 = startstr.indexOf(' ');

        if (startstr.substr(idx2 + 1, 2) == "06") {
            return startstr.substring(idx1 + 1, idx2) + " 白天";
        } else if (startstr.substr(idx2 + 1, 2) == "18") {
            return startstr.substring(idx1 + 1, idx2) + " 晚上";
        } else {
            return "現在";
        }
    },

    updateaApi: function () {
        for (var i = 0; i < app.trailerData.length; i++) {
            var li = $("<li>");
            li.append($("<h1>").text(app.trailerData[i].拖吊責任區));
            $("<span>").addClass("ui-li-count").text(app.trailerData[i].拖吊保管場名稱).appendTo(li);
            $("#apiData").append(li);
        }
        $("#apiData").listview("refresh");
    },
};

app.initialize();