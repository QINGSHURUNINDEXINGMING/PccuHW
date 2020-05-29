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
    insertArea: function () {
        $('#guide').hide();
        $.each(app.trailerData, (index, js) => {
            $.each(js, (key, value) => {
                if (key === "拖吊責任區" && value !== "")
                    $('<option/>', { 'value': value.slice(3, 10), 'text': value.slice(3, 10) }).appendTo('#area')
            })
        });
        $('<option/>', { 'value': "", 'text': "其他" }).appendTo('#area')
        $("#area").selectmenu("refresh", true);
    },

    onDeviceReady: function () {
        app.checkConnection();

        var trailerUrl = "https://data.taipei/opendata/datalist/apiAccess?scope=resourceAquire&rid=24045907-b7c3-4351-b0b8-b93a54b55367";
        $.getJSON(trailerUrl, function (response) {
            app.trailerData = response.result.results;
            app.insertArea();
            $('#area').change(() => {
                $('ul').empty();
                var search = $('#area option:selected').val()
                app.updateaApi(search);
            });
        })
    },

    checkConnection: function () {
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
                    li += "<span>" + key + "</span>" + " : " + value + "<br>";
                });
                li += "</li>";
                $("#apiData").append(li)
                $("span").css("font-weight", "Bold");
            }
        });
        $("#apiData").listview("refresh");
    },
};

app.initialize();