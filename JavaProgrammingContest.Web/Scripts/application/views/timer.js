var TimerView;

$(document).ready(function () {
    TimerView = Backbone.View.extend({
        // Representing element
        el: $("#TimePane"),

        initialize: function () {
            this.factory = BuilderFactory;
        },

        render: function () {
            if (this.current) {
                if (this.current.build) {
                    console.log('Received build info, populating console.');
                    this.$el.find('#buildStream').html('<div style="color: red;">' + this.current.build.error + '</div>' + this.current.build.output);
                }
                if (this.current.run) {
                    console.log('Received runner info.');
                    this.$el.find('#outputStream').text(this.current.run.output);
                    this.$el.find('#errorStream').text(this.current.run.error);
                }
            }
        }
    });
});