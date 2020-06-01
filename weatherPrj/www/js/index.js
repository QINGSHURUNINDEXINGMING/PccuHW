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
        plugin.google.maps.environment.setEnv({
            'API_KEY_FOR_BROWSER_RELEASE': 'AIzaSyBAlgI7QVaE51UD6jkgib0tSpxwK-z4bXg',
            'API_KEY_FOR_BROWSER_DEBUG': ''  // optional
        });

        $.getJSON(trailerUrl, (response) => {
            app.trailerData = response.result.results;
            app.insertArea();

            $('#area').change(() => {
                $('ul').empty();
                app.updateaApi($('#area ').val());
                app.speedDial($('#area option:selected').val());
                app.setMap($('#area option:selected').val());
            });
        })
    },

    setMap: (search) => {

        var map1 = plugin.google.maps.Map.getMap($("#map_canvas").get(0));
        var map2 = plugin.google.maps.Map.getMap($("#map_canvas").get(0));
        var map3 = plugin.google.maps.Map.getMap($("#map_canvas").get(0));
        var map4 = plugin.google.maps.Map.getMap($("#map_canvas").get(0));
        var map5 = plugin.google.maps.Map.getMap($("#map_canvas").get(0));
        var map6 = plugin.google.maps.Map.getMap($("#map_canvas").get(0));
        var map7 = plugin.google.maps.Map.getMap($("#map_canvas").get(0));
        var map8 = plugin.google.maps.Map.getMap($("#map_canvas").get(0));
        $.each(app.trailerData, (index, value) => {
            if (app.trailerData[index].拖吊責任區.slice(3, 10) == search && search != "") {
                switch (search) {
                    case '士林區、北投區':
                        map1.animateCamera({
                            target: { lat: 25.096813, lng: 121.513799 },
                            zoom: 17,
                            tilt: 60,
                            bearing: 140,
                            duration: 5000
                        });
                        map11 = map1.addMarker({
                            position: { lat: 25.096813, lng: 121.513799 },
                            title: "勝倫國際有限公司",
                            snippet: "地址: 臺北市士林區基河路328號旁空地 (基河二停車場內)",
                            animation: plugin.google.maps.Animation.BOUNCE
                        })
                        map11.showInfoWindow();
                        break;
                    case '中正區、萬華區':
                        map2.animateCamera({
                            target: { lat: 25.0330301, lng: 121.5141138 },
                            zoom: 17,
                            tilt: 60,
                            bearing: 140,
                            duration: 5000
                        });
                        map22 = map2.addMarker({
                            position: { lat: 25.0330301, lng: 121.5141138 },
                            title: "晨旺有限公司",
                            snippet: "地址: 臺北市中正區南昌路1段5號1樓(仰德大樓停車場內)",
                            animation: plugin.google.maps.Animation.BOUNCE
                        });
                        map22.showInfoWindow();
                        break;
                    case '大安區、文山區':
                        map3.animateCamera({
                            target: { lat: 25.0359794, lng: 121.5418411 },
                            zoom: 17,
                            tilt: 60,
                            bearing: 140,
                            duration: 5000
                        });
                        map33 = map3.addMarker({
                            position: { lat: 25.0359794, lng: 121.5418411 },
                            title: "永耀汽車有限公司",
                            snippet: "地址: 臺北市大安區信義路3段147巷10號空地(大安文山保管場停車場內)",
                            animation: plugin.google.maps.Animation.BOUNCE
                        });
                        map33.showInfoWindow();
                        break;
                    case '松山區、內湖區':
                        map4.animateCamera({
                            target: { lat: 25.061254, lng: 121.579902 },
                            zoom: 17,
                            tilt: 60,
                            bearing: 140,
                            duration: 5000
                        });
                        map44 = map4.addMarker({
                            position: { lat: 25.061254, lng: 121.579902 },
                            title: "富督企業有限公司",
                            snippet: "地址: 臺北市內湖區新湖二路191巷17號斜對面空地 (天成舊宗停車場內)",
                            animation: plugin.google.maps.Animation.BOUNCE
                        });
                        map44.showInfoWindow();
                        break;
                    case '中山區、大同區':
                        map5.animateCamera({
                            target: { lat: 25.0534596, lng: 121.5377808 },
                            zoom: 17,
                            tilt: 60,
                            bearing: 140,
                            duration: 5000
                        });
                        map55 = map5.addMarker({
                            position: { lat: 25.0534596, lng: 121.5377808 },
                            title: "亞立國際有限公司",
                            snippet: "地址: 臺北市中山區建國北路2段11巷新光大樓後對面空地(便利停車場建國站內)",
                            animation: plugin.google.maps.Animation.BOUNCE
                        });
                        map55.showInfoWindow();
                        break;
                    default:
                        console.log(`Sorry, we are out of ${expr}.`);
                }
            }
        });
        if (search == "") {
            map6.animateCamera({
                target: { lat: 25.067771, lng: 121.5648153 },
                zoom: 17,
                tilt: 60,
                bearing: 140,
                duration: 5000
            });
            // map7.animateCamera({
            //     target: { lat: 25.0708724, lng: 121.5581573 },
            //     zoom: 17,
            //     tilt: 60,
            //     bearing: 140,
            //     duration: 5000
            // });
            // map8.animateCamera({
            //     target: { lat: 25.0611419, lng: 121.5789157 },
            //     zoom: 17,
            //     tilt: 60,
            //     bearing: 140,
            //     duration: 5000
            // })
            var map = [
                map1 = map6.addMarker({
                    position: { lat: 25.067771, lng: 121.5648153 },
                    title: "公有撫遠逾期車輛放置場",
                    snippet: "地址: 臺北市松山區撫遠街419-1號旁",
                    animation: plugin.google.maps.Animation.BOUNCE
                }),

                map2 = map7.addMarker({
                    position: { lat: 25.0708724, lng: 121.5581573 },
                    title: "公有濱江第二逾期車輛放置場",
                    snippet: "地址: 臺北市松山區濱江街888-1號對面(僅描述相對位置)",
                    animation: plugin.google.maps.Animation.BOUNCE
                }),
                map3 = map8.addMarker({
                    position: { lat: 25.0611419, lng: 121.5789157 },
                    title: "交通大隊公有保管場",
                    snippet: "地址: 臺北市內湖區新湖一路193號",
                    animation: plugin.google.maps.Animation.BOUNCE
                })];
            map[1].showInfoWindow();
            map[2].showInfoWindow();
            map[0].showInfoWindow();
        }

    },

    speedDial: (search) => {
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
                    li += (key === "_id") ? "" :
                        "<span>" + key + "</span>" + " : " + ((value === "") ? "其它" : (key === "電話" ? value.slice(0, 12) + "<button id='" + index + "'>撥號</button>" : value)) + "<br>";
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
