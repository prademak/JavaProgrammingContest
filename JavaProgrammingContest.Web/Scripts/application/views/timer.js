var TimerView;

$(document).ready(function () {
    TimerView = Backbone.View.extend({
        // Representing element
        el: $("#TimerPane"),

        running: true,
        start: null,
        time: 0,
        maxSolveTime: 0,
        app: null,

        initialize: function (maxSolveTime, application) {
            this.app = application;
            this.maxSolveTime = maxSolveTime;
            this.start = new Date();
            this.render();
        },

        render: function() {
            var ns = this;

            var curTime = new Date();
            var difference = Math.floor((curTime.getTime() - this.start.getTime()) / 1000);

            var hours = 0;
            var minutes = 0;

            while (difference >= (60 * 60)) {
                difference -= (60 * 60);
                hours++;
            }

            while (difference >= 60) {
                difference -= 60;
                minutes++;
            }

            var countDown = this.maxSolveTime;
            var maxHours = 0;
            var maxMinutes = 0;
            
            while (countDown >= (60 * 60)) {
                countDown -= (60 * 60);
                maxHours++;
            }
            
            while (countDown >= 60) {
                countDown -= 60;
                maxMinutes++;
            }

            var maxSeconds = countDown;

            var countHours = maxHours - hours;
            var countMinutes = maxMinutes - minutes;
            var countSeconds = maxSeconds - difference;

            if (countSeconds < 0) {
                if (countMinutes > 0) {
                    countMinutes--;
                    countSeconds += 60;
                } else {
                    countSeconds = 0;
                    this.stop();
                }
            }

            if (countMinutes < 0) {
                countMinutes += 60;

                if (countHours > 0) {
                    countHours--;
                    countMinutes += 60;
                } else {
                    countMinutes = 0;
                }
            }

            this.$el.find('.hours').text((countHours < 10) ? '0' + countHours : countHours);
            this.$el.find('.minutes').text((countMinutes < 10) ? '0' + countMinutes : countMinutes);
            this.$el.find('.seconds').text((countSeconds < 10) ? '0' + countSeconds : countSeconds);

            if (this.running) setTimeout(function () { ns.render.call(ns); }, 1000);
            
            if (countHours == 0 && countMinutes == 0 && countSeconds == 0 && this.running) {
               this.timeRunOut();
                this.stop();
            }
        },

        stop: function() {
            this.running = false;
        },
        
        timeRunOut: function () {
            var ns = app;

            noty({
                text: 'Sorry, you ran out of time!', type: 'confirm', layout: 'topCenter', modal: true, buttons: [
                     {
                         addClass: 'btn btn-danger', text: 'Ok', onClick: function ($noty) {
                             $noty.close();
                             API.Progress.stop();
                             ns.assignments.nextAssignment();

                             // No assignment in progress anymore
                             ns.assignments.started = false;

                             // Disable all tabs
                             ns.$el.find('.tabbable.tabs-below li:not(:first-child)')
                                .addClass('disabled');


                             ns.$el.find('li#startTab').removeClass('hide');
                             ns.$el.find('li#startTab').removeClass('disabled');
                             // Change view to assignment
                             ns.changeView('start');

                             // Enable the start time button
                             ns.$el.find('#AssignmentPane .properties a')
                                .removeClass('disabled')
                                .text('Start the Time!');
                             // Enable the start time button
                             ns.$el.find('#StartPane .properties a')
                                .removeClass('disabled')
                                .text('Start the Time!');
                 
                     }
                ]
            });

        }
    });
});