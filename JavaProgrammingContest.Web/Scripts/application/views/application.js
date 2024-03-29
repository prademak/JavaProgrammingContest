﻿var ApplicationView;

$(document).ready(function () {
    ApplicationView = Backbone.View.extend({
        el: $(".editor-row").get(0),

        console: null,

        editor: null,
        
        assignments: null,

        timer: null,

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

            "click .pane.editor #AssignmentPane .properties .btn": "startAssignment",

            "click .pane.editor #StartPane .properties .btn": "startAssignment"
            //"keypress #new-todo": "createOnEnter",
            //"click #clear-completed": "clearCompleted",
            //"click #toggle-all": "toggleAllComplete"
        },

        initialize: function () {
            this.console = new ConsoleView();
            this.editor = new EditorView();
            this.assignments = new AssignmentView({ model: new AssignmentCollection() });
            var ns = this;
            this.assignments.on('select', function (mdl) { ns.changeAssignment.call(ns, mdl); });
            this.assignments.on('next', function () { ns.showNextAssignment.call(ns); });
            
            // Ask if user really wants to leave when assignment is in progress
            $(document).unload(function() {
                if (ns.assignments.started == true)
                    return confirm('Wil je echt stoppen met het maken van de opdracht? (Je kunt hem niet herstarten!)');
            });
        },
        
        // Re-rendering the App just means refreshing the statistics -- the rest
        // of the app doesn't change.
        render: function () {
            // Firstly check the assignments
            this.assignments.model.fetch({ url: '/api/assignments' });
            
        },

        changeTheme: function(e) {
            this.editor.setOption("theme", $(e.currentTarget).attr('data-theme'));
        },

        changeView: function (view) {
            // Check validity
            if (_.indexOf(['start', 'editor', 'console', 'assignment', 'splitscreen'], view) == -1) {
                console.warn('Unknown view type "' + view + '".');
                return false;
            }

            // Check disabled state
            if (this.$el.find('.tabbable.tabs-below a[href~=#' + view + ']').parent().hasClass('disabled')) return false;

            // Do general stuff
            this.$el.find('.tabbable.tabs-below .active').removeClass('active');
            this.$el.find('.tabbable.tabs-below a[href~=#'+view+']').parent().addClass('active');

            this.$el.find('.activePane').removeClass('activePane');
            this.$el.find('#'+view.charAt(0).toUpperCase()+view.substr(1)+'Pane').addClass('activePane');

            if (view == 'start') {
               
            } else if (view == 'editor') {
                // Remove splitscreen
                this.$el.find('.splitscreen').removeClass('splitscreen');
            } else if (view == 'console') {
                // Remove splitscreen
                this.$el.find('.splitscreen').removeClass('splitscreen');
            } else if(view == 'assignment'){
                
            } else if (view == 'splitscreen') {
                // Activate splitscreen
                this.$el.find('#EditorPane, #ConsolePane').addClass('splitscreen');
                this.editor.refresh();
            }

            return true;
        },
        
        changeViewEvent: function(e) {
            this.changeView($(e.currentTarget).attr('href').substring(1));
            
            e.stopPropagation();
            e.preventDefault();
            return false;
        },

        buildCode: function (e) {
            this.console.build(this.editor.getContent()); // Build on server
            this.console.switchView('build'); // Switch console tab

            e.stopPropagation();
            e.preventDefault();
            return false;
        },
        runCode: function (e) {
            var ns = this;

            // Build on server
            this.console.run(this.editor.getContent(), function (result) {
                // Switch console tab
                if (result.build.error.length > 0)
                    ns.console.switchView('build');
                else if (result.run.error.length > 0)
                    ns.console.switchView('error');
                else ns.console.switchView('output');
            });

            e.stopPropagation();
            e.preventDefault();
            return false;
        },
        
        changeAssignment: function (mdl) {
            this.editor.setContent(mdl.get('CodeGiven'));
        },
        
        startAssignment: function (e) {
            var ns = this;

            // Start progress of assignment on the server
            (new ProgressModel({
                Id: this.assignments.current.id
            })).save(null, {
                success: function() {
                    (function() {
                        // Check and set the application state
                        if (this.assignments.started == true) return false;
                        else this.assignments.started = true;

                        // Show the timer
                        this.timer = new TimerView(this.assignments.current.get('MaxSolveTime'), this);

                        // Enable all the editor tabs
                        this.$el.find('.tabbable.tabs-below li')
                            .removeClass('disabled');
                        // Enable all the editor tabs
                        this.$el.find('li#startTab').addClass('hide');
                      
                        // Change the view to splitscreen mode
                        this.changeView('assignment');

                        // Disable the start time button
                        this.$el.find('#AssignmentPane .properties a')
                            .addClass('disabled')
                            .text('...in progress.');

                        console.log('Started Assignment with id: ' + this.assignments.current.id);
                    }).call(ns);
                },
                error: function () {
                    noty({
                        text: 'Fout bij het starten van de opdracht; de gebruiker is reeds bezig met een opdracht.',
                        type: 'error', layout: 'topRight'
                    });
                }
            });

            e.stopPropagation();
            e.preventDefault();
            return false;
        },
        
        submitAssignment: function (e) {
            var ns = this;

            // Notify the user
            $('#assignmentSubmitModal').modal({ backdrop: true, keyboard: false, show: true });
            $('#assignmentSubmitModal .btn-success').click(function () {
                // Stop the timer
                ns.timer.stop();
                ns.timer = null;

                // Submit the assignment using the model
                var succ = (new CodeSubmitModel({ Id: ns.assignments.current.get('Id'), Code: ns.editor.getContent() })).save();

                if (succ != false) {
                    noty({
                        text: 'Bedankt voor het inzenden!\nJe inzending "' + ns.assignments.current.get('Title') + '" is ontvangen en zal automatisch worden beoordeelt. Zodra dit is gebeurd verschijnt uw resultaat in het scorebord.',
                        type: 'success', layout: 'topRight'
                    });
                } else {
                    console.log('FAILED TO SUBMIT THE ASSIGNMENT!');
                    noty({
                        text: 'Sorry, het is niet gelukt om de opdracht "' + ns.assignments.current.get('Title') + '" in te zenden! Neem contact op met een serverbeheerder.',
                        type: 'error', layout: 'topRight'
                    });
                }

                // Find current list-item and mark it as done
                ns.$el.find('li[data-assignment=' + ns.assignments.modelIndex + ']').addClass('done');
                 
                // Disable all tabs
                ns.$el.find('.tabbable.tabs-below li:not(:first-child)')
                   .addClass('disabled');

                ns.$el.find('li#startTab').removeClass('hide');

                // Change view to assignment
                ns.changeView('start');

                // Load next assignment
                ns.assignments.nextAssignment();

                // No assignment in progress anymore
                ns.assignments.started = false;
         
                // Enable the start time button
                ns.$el.find('#AssignmentPane .properties a')
                   .removeClass('disabled')
                   .text('Start de tijd!');

                // Enable the start time button
                ns.$el.find('#StartPane .properties a')
                   .removeClass('disabled')
                   .text('Start de tijd!');
            });

            e.stopPropagation();
            e.preventDefault();
            return false;
        },
        
        cancelAssignment: function (e) {
            var ns = this;
            noty({
                text: 'Weet je zeker dat je de opdracht wilt stoppen?\nU kunt hem niet opnieuw openen/maken!', type: 'confirm', layout: 'topCenter', modal: true, buttons: [
                     {
                         addClass: 'btn btn-danger', text: 'Ok', onClick: function ($noty) {
                             $noty.close();
                             API.Progress.stop();
                             ns.showNextAssignment();
                         }
                     },
                    {
                        addClass: 'btn btn-primary', text: 'Cancel', onClick: function ($noty) {
                            $noty.close();
                        }
                    }
                ]
            });

            e.stopPropagation();
            e.preventDefault();
            return false;
        },
        showNextAssignment: function () {
            var ns = this;

            if (ns.timer != null) {
                ns.timer.stop();
                ns.timer = null;
            }
          
            // Find current list-item and mark it as done
            ns.$el.find('li[data-assignment=' + ns.assignments.modelIndex + ']').addClass('done');
                 
            // Disable all tabs
            ns.$el.find('.tabbable.tabs-below li:not(:first-child)')
               .addClass('disabled');

            ns.$el.find('li#startTab').removeClass('hide');

            // Change view to assignment
            ns.changeView('start');

            // Load next assignment
            ns.assignments.nextAssignment();

            // No assignment in progress anymore
            ns.assignments.started = false;
         
            // Enable the start time button
            ns.$el.find('#AssignmentPane .properties a')
               .removeClass('disabled')
               .text('Start de tijd!');

            // Enable the start time button
            ns.$el.find('#StartPane .properties a')
               .removeClass('disabled')
               .text('Start de tijd!');
        }
    });
}); 