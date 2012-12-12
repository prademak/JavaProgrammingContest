var ConsoleView;

$(document).ready(function () {
    ConsoleView = Backbone.View.extend({
        // Representing element
        el: $("#ConsolePane"),
        
        // Builder Factory for building the code
        factory: null,

        // Build Info
        current: null,

        events: {
            //"keypress #new-todo": "createOnEnter"
        },

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
        },
        submit: function (code) {
            this.current = null;
            var ns = this;
            this.factory.submit(code, function (result) {
               
                
           
                console.log(result, ns.current);
                ns.render();
            });
        },
        run: function (code) {
            this.current = null;
            var ns = this;
            this.factory.run(code, function(result) {
                ns.current = {
                    build: {
                        time: result.BuildResult.CompileTime,
                        error: result.BuildResult.Error,
                        output: result.BuildResult.Output
                    },
                    run: {
                        time: result.RunTime,
                        error: result.Error,
                        output: result.Output
                    }
                };
                console.log(result, ns.current);
                ns.render();
            });
        },
        
        build: function(code){
            this.current = null;
            var ns = this;
            this.factory.build(code, function (result) {
                ns.current = {
                    build: {
                        time: result.CompileTime,
                        error: result.Error,
                        output: result.Output
                    }
                };
                console.log(ns.current, result);
                ns.render();
            });
        }
    });
});