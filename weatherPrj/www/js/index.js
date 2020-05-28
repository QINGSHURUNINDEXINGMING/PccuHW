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
var app = {
    trailerData: "",
    weatherData: "",
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
                if (key === "拖吊責任區" && item[key] !== "")
                    $('<option/>', { 'value': item[key].slice(3, 10), 'text': item[key].slice(3, 10) }).appendTo('#area')
            });
        });
        $("#area").selectmenu("refresh", true);
    },
    onDeviceReady: function () {
        app.checkConnection();

        var trailerUrl = "https://data.taipei/opendata/datalist/apiAccess?scope=resourceAquire&rid=24045907-b7c3-4351-b0b8-b93a54b55367";


        $.getJSON(trailerUrl, function (response) {
            app.trailerData = response.result.results;
            app.updateaApi();
            console.log(app.trailerData);
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

    updateaApi: function () {
        // var li = $("<li>");
    
      
        // $.each(app.trailerData, (index, value) => {
        //     $.each(value, (key, value) => {
        //         li.append($("<p>").text(key + ": " + value).css("font-weight", "Bold"));
        //     });
        //     $("#apiData").append(li);
        // });
        var li = $("<li>");
    
      
        $.each(app.trailerData, (index, value) => {
            $("#apiData").append('<li>');
            $("#apiData").css("font-weight", "Bold");
            $.each(value, (key, value) => {
                $("#apiData").append( key + " : " + value).append('<br>')
            });
            $("#apiData").append('</li>');
            // $("#apiData").append(li);
        });
    
        // $.each(data.result, function(i, item) {
        //     alert(data.result[i].PageName);
        // });
        // var fakeArray = { "length": 2, 0: "Addy", 1: "Subtracty" };

        // // Therefore, convert it to a real array
        // var realArray = $.makeArray(fakeArray)

        // // Now it can be used reliably with $.map()
        // $.map(realArray, function (val, i) {
        //     // Do something
        // });

        // app.trailerData.forEach((item) => {
        //     Object.keys(item).forEach((key) => {
        //         li.append($("<p>").text(key + ": " + item[key]).css("font-weight", "Bold"));
        //         console.log(key, item[key])
        //     });
        //     $("#apiData").append(li);
        // });
        // for (var i = 0; i < app.trailerData.length; i++) {
        //     var li = $("<li>");
        //     li.append($("<h1>").text(app.trailerData[i].拖吊責任區));
        //     $("<span>").addClass("ui-li-count").text(app.trailerData[i].拖吊保管場名稱).appendTo(li);
        //     $("#apiData").append(li);
        // }
        $("#apiData").listview("refresh");
    },
};

app.initialize();