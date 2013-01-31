var TimerView;

$(document).ready(function () {
    TimerView = Backbone.View.extend({
        //constants
        hourInSecondes: 3600,
        minuteInSeconds: 60,

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

            while (difference >= this.hourInSecondes) {
                difference -= this.hourInSecondes;
                hours++;
            }

            while (difference >= this.minuteInSeconds) {
                difference -= this.minuteInSeconds;
                minutes++;
            }

            var countDown = this.maxSolveTime;
            var maxHours = 0;
            var maxMinutes = 0;
            
            while (countDown >= this.hourInSecondes) {
                countDown -= this.hourInSecondes;
                maxHours++;
            }
            
            while (countDown >= this.minuteInSeconds) {
                countDown -= this.minuteInSeconds;
                maxMinutes++;
            }

            var maxSeconds = countDown;

            var countHours = maxHours - hours;
            var countMinutes = maxMinutes - minutes;
            var countSeconds = maxSeconds - difference;

            if (countSeconds < 0) {
                if (countMinutes > 0) {
                    countMinutes--;
                    countSeconds += this.minuteInSeconds;
                } else {
                    countSeconds = 0;
                    this.stop();
                }
            }

            if (countMinutes < 0) {
                countMinutes += this.minuteInSeconds;

                if (countHours > 0) {
                    countHours--;
                    countMinutes += this.minuteInSeconds;
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
                text: 'Sorry, de timer is afgelopen! U heeft de opdracht niet binnen de tijd kunnen afronden. De opdracht zal nu worden afgesloten.', type: 'confirm', layout: 'topCenter', modal: true, buttons: [
                     {
                         addClass: 'btn btn-danger', text: 'Ok', onClick: function ($noty) {
                             $noty.close();

                         }
                        
                     }
                ]
            });
            ns.showNextAssignment();

        }
    });
});