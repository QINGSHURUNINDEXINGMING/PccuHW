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
    // Application Constructor
    initialize: function () {
        document.addEventListener('deviceready', this.onDeviceReady.bind(this), false);
    },
    // deviceready Event Handler
    //
    // Bind any cordova events here. Common events are:
    // 'pause', 'resume', etc.
    insertArea: () => {
        $('#guide').hide();
        $.each(app.trailerData, (index, js) => {
            $.each(js, (key, value) => {
                if (key === "拖吊責任區" && value !== "")
                    $('<option/>', { 'value': value.slice(3, 10), 'text': value.slice(3, 10) }).appendTo('#area')
            })
        });
        $('<option/>', { 'value': "", 'text': "其他" }).appendTo('#area')
        $('#area').selectmenu().selectmenu('refresh', true);
    },

    onDeviceReady: function () {
        app.checkConnection();
        var trailerUrl = "https://data.taipei/opendata/datalist/apiAccess?scope=resourceAquire&rid=24045907-b7c3-4351-b0b8-b93a54b55367";
        $.getJSON(trailerUrl, (response) => {
            app.trailerData = response.result.results;
            app.insertArea();
            $('#area').change(() => {
                $('ul').empty();
                app.updateaApi($('#area option:selected').val());
                app.speedDial($('#area option:selected').val());
                // $("#api").on("pageshow", () => {
                plugin.google.maps.environment.setEnv({
                    'API_KEY_FOR_BROWSER_RELEASE': 'AIzaSyBAlgI7QVaE51UD6jkgib0tSpxwK-z4bXg',
                    'API_KEY_FOR_BROWSER_DEBUG': ''  // optional
                });
                // Create a Google Maps native view under the map_canvas div.
                var map = plugin.google.maps.Map.getMap($("#map_canvas").get(0));
                // If you click the button, do something...
                // $("#button").get(0).addEventListener("click", () => {
                // Move to the position with animation
                map.animateCamera({
                    target: { lat: 25.0487187, lng: 121.5857417 },
                    zoom: 17,
                    tilt: 60,
                    bearing: 140,
                    duration: 5000
                });
                // Add a maker
                var marker = map.addMarker({
                    position: { lat: 25.0487187, lng: 121.5857417 },
                    title: "勝倫國際有限公司",
                    snippet: "地址: 臺北市士林區基河路328號旁空地 (基河二停車場內)",
                    animation: plugin.google.maps.Animation.BOUNCE
                });
                // Show the info window
                marker.showInfoWindow();
                // });
                // });

            });
        })
    },

    speedDial: (search) => {
        console.log(search)
        $.each(app.trailerData, (index, value) => {
            if (app.trailerData[index].拖吊責任區.slice(3, 10) == search) {
                $.each(value, (key, value) => {
                    if (key == "電話") {
                        $("#" + index).click(() => {
                            window.open("tel:" + value.slice(0, 12), '_system');
                        })
                    }
                });
            }
        });
    },

    checkConnection: () => {
        var networkState = navigator.connection.type;
        if (networkState === Connection.NONE) {
            alert("沒有網路連線...");
            navigator.app.exitApp(); // 離開應用程式
        }
    },

    updateaApi: (search) => {
        $.each(app.trailerData, (index, value) => {
            var li = "";
            li += "<li>";
            if (app.trailerData[index].拖吊責任區.slice(3, 10) == search) {
                $.each(value, (key, value) => {
                    li += (key === "_id") ? "" : "<span>" + key + "</span>" + " : " + ((value === "") ? "其它" : (key === "電話" ? value.slice(0, 12) + "<button id='" + index + "'>撥號</button>" : value)) + "<br>";
                });
                li += "</li>";
                $("#apiData").append(li)
            }
        });
        $("li").css("white-space", "normal");
        $("span").css("font-weight", "Bold");
        $("#apiData").listview("refresh");
    },
};

app.initialize();