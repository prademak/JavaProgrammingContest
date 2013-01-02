var TimerView;

$(document).ready(function () {
    TimerView = Backbone.View.extend({
        // Representing element
        el: $("#TimerPane"),

        running: true,
        start: null,
        time: 0,
        maxSolveTime: 0,

        initialize: function (maxSolveTime) {
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
                countSeconds += 60;

                if (countMinutes > 0) {
                    countMinutes--;
                }
            }

            if (countMinutes < 0) {
                countMinutes += 60;

                if (countHours > 0) {
                    countHours--;
                }
            }

            this.$el.find('.hours').text((countHours < 10) ? '0' + countHours : countHours);
            this.$el.find('.minutes').text((countMinutes < 10) ? '0' + countMinutes : countMinutes);
            this.$el.find('.seconds').text((countSeconds < 10) ? '0' + countSeconds : countSeconds);

            if (this.running) setTimeout(function () { ns.render.call(ns); }, 1000);
        },

        stop: function() {
            this.running = false;
        }
    });
});