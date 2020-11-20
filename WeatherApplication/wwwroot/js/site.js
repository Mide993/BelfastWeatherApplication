$(document).ready(function () {
    populateWeatherData(1);
});

//Ajax call to the server method to retrieve the data from the MetaWeather API.
function populateWeatherData(value) {
    $.ajax({
        type: "GET",
        url: "/Home/GetWeatherData",
        dataType: "json",
        success: function (data) {
            if (data !== null) {
                //A check for existing elements to be erased and replaced with updated data.
                if (value === 0) {
                    $("#weatherData").empty();
                }
                for (var i = 0; i <= data.consolidated_weather.length - 1; i++) {
                    //Create an element for each weather object.
                    var model = data.consolidated_weather[i];
                    $("#weatherData").append(
                        '<div class="weatherDiv" style="float:left; width: 32%; height: 10%; margin-top:5%"><h5> Date: ' + model.applicable_date + '</h5><h6>' + model.weather_state_name + '</h6><h6>Temp: ' + Math.round(model.the_temp) + '</h6><p><img src="https://www.metaweather.com/static/img/weather/png/' + model.weather_state_abbr + '.png" style="height: 50px; width: 50px" /></p></div>'
                    );
                }
            }
            else {
                console.log("There has been an error retrieving the API data")
            }
        },
        error: function (e) {
            alert("Problem with retrieving data:" + e);
        }
    });
}

//A function to initialise the pull to load mechanic
window.onload = function () {
    WebPullToRefresh.init({
        loadingFunction: loadingFunction
    });
};

//Runs the refresh of data on the pull down function
var loadingFunction = function () {
    return new Promise(function (resolve, reject) {
        
        populateWeatherData(0);
        
        if (true /* if the loading worked */) {
            resolve();
        } else {
            reject();
        }
    });
};