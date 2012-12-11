var ApplicationView;

$(document).ready(function () {
    ApplicationView = Backbone.View.extend({
        el: $(".editor-row").get(0),

        console: null,

        editor: null,
        
        assignments: null,

        // Our template for the line of statistics at the bottom of the app.
        //statsTemplate: _.template($('#stats-template').html()),

        // Delegated events for creating new items, and clearing completed ones.
        events: {
            "click .pane.editor .dropdown-menu a": "changeTheme",
            "click .tabbable.tabs-below li a": "changeViewEvent",
            
            "click .pane.editor .btn-toolbar .btn[href=#submit]": "submitAssignment",
            "click .pane.editor .btn-toolbar .btn[href=#cancel]": "cancelAssignment",
            "click .pane.editor .btn-toolbar .btn[href=#build]": "buildCode",
            "click .pane.editor .btn-toolbar .btn[href=#run]": "runCode",
            
            "click .pane.editor #AssignmentPane .properties .btn": "startAssignment"
            //"keypress #new-todo": "createOnEnter",
            //"click #clear-completed": "clearCompleted",
            //"click #toggle-all": "toggleAllComplete"
        },

        initialize: function () {
            this.console = new ConsoleView();
            this.editor = new EditorView();
            this.assignments = new AssignmentView({ model: new AssignmentCollection() });
            var ns = this;
            this.assignments.on('select', function (mdl) { ns.setAssignment.call(ns, mdl); });
            
            
        },

        // Re-rendering the App just means refreshing the statistics -- the rest
        // of the app doesn't change.
        render: function () {
            // Firstly check the assignments
            this.assignments.model.fetch({ url: '/api/assignments' });
        },
        
        setAssignment: function(mdl) {
            this.editor.setContent(mdl.get('CodeGiven'));
        },

        changeTheme: function(e) {
            this.editor.setOption("theme", $(e.currentTarget).attr('data-theme'));
        },

        changeView: function(view) {
            if (view == 'editor') {
                this.$el.find('.tabbable.tabs-below .active').removeClass('active');
                this.$el.find('.tabbable.tabs-below a[href~=#editor]').parent().addClass('active');

                this.$el.find('.activePane').removeClass('activePane');
                this.$el.find('#EditorPane').addClass('activePane');
                
                // Remove splitscreen
                this.$el.find('.splitscreen').removeClass('splitscreen');
                this.editor.refresh();
            } else if (view == 'console') {
                this.$el.find('.tabbable.tabs-below .active').removeClass('active');
                this.$el.find('.tabbable.tabs-below a[href~=#console]').parent().addClass('active');

                this.$el.find('.activePane').removeClass('activePane');
                this.$el.find('#ConsolePane').addClass('activePane');
                
                // Remove splitscreen
                this.$el.find('.splitscreen').removeClass('splitscreen');
            } else if(view == 'assignment'){
                this.$el.find('.tabbable.tabs-below .active').removeClass('active');
                this.$el.find('.tabbable.tabs-below a[href~=#assignment]').parent().addClass('active');

                this.$el.find('.activePane').removeClass('activePane');
                this.$el.find('#AssignmentPane').addClass('activePane');
            } else if (view == 'splitscreen') {
                this.$el.find('.tabbable.tabs-below .active').removeClass('active');
                this.$el.find('.tabbable.tabs-below a[href~=#splitscreen]').parent().addClass('active');

                this.$el.find('.activePane').removeClass('activePane');
                this.$el.find('#SplitscreenPane').addClass('activePane');
                
                // Activate splitscreen
                this.$el.find('#EditorPane, #ConsolePane').addClass('splitscreen');
                this.editor.refresh();
            } else {
                console.warn('Unknown view type "'+view+'".');
            }
        },
        
        changeViewEvent: function(e) {
            this.changeView($(e.currentTarget).attr('href').substring(1));
            return false;
        },

        buildCode: function() {
            this.console.build(this.editor.getContent());
            return false;
        },
        runCode: function () {
            this.console.run(this.editor.getContent());
            return false;
        },

        submitAssignment: function() {
            var newCodeSubmission = new AssignmentModel({
                Code: this.editor.getContent()
            });
            newCodeSubmission.save();
            return false;
        },
        
        cancelAssignment: function () {
            var ns = this;
            noty({
                text: 'Are you sure you want to stop this assignment?\nYou cannot reopen it when you give up!', type: 'confirm', layout: 'topCenter', modal: true, buttons: [
                     {
                         addClass: 'btn btn-danger', text: 'Ok', onClick: function ($noty) {
                             $noty.close();
                             ns.assignments.nextAssignment();
                         }
                     },
                    {
                        addClass: 'btn btn-primary', text: 'Cancel', onClick: function ($noty) {
                            $noty.close();
                            noty({ text: 'You clicked "Cancel" button', type: 'error' });
                        }
                    }
                ]
            });
            //this.setAssignment(this.assignments.current);
            return false;
        },

        startAssignment: function() {
            // TODO STart the timer <Alexender>
            return false;
        }
    });
});