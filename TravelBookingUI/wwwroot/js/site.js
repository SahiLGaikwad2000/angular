// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
$(function () {
    $("#DateRequestDeadLine").datepicker({
        var endDate = "", noOfDaysToAdd = 5, count = 0;
        while(count <noOfDaysToAdd) {
            endDate = new Date(startDate.setDate(startDate.getDate() + 1));
            if (endDate.getDay() != 0 && endDate.getDay() != 6) {
                //Date.getDay() gives weekday starting from 0(Sunday) to 6(Saturday)
                count++;
            }
        }
    
                 var dd = String(endDate.getDate()).padStart(2, '0');
        var mm = String(endDate.getMonth() + 1).padStart(2, '0'); //January is 0!
        var yyyy = endDate.getFullYear();
        endDate = dd + '/' + mm + '/' + yyyy;
        var minDate = yyyy + '-' + mm + '-' + dd;

        document.getElementById("DateRequestDeadLine").style.display = "block";
        document.getElementById("DateRequestDeadLine").min = minDate;
    });


})

 
