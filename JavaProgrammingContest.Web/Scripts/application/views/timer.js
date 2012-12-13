var TimerView;

$(document).ready(function () {
    TimerView = Backbone.View.extend({
        // Representing element
        el: $("#TimerPane"),

        running: true,
        start: null,
        time: 0,

        initialize: function () {
            this.start = new Date();
            this.render();
        },

        render: function () {
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

            this.$el.find('.hours').text((hours < 10) ? '0' + hours : hours);
            this.$el.find('.minutes').text((minutes < 10) ? '0' + minutes : minutes);
            this.$el.find('.seconds').text((difference < 10) ? '0' + difference : difference);

            if (this.running) setTimeout(function () { ns.render.call(ns); }, 1000);
        },

        stop: function () {
            this.running = false;
        }
    });
});