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
            "click .pane.editor .btn-toolbar .btn[href=#reset]": "resetAssignment",
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
            } else if (view == 'console') {
                this.$el.find('.tabbable.tabs-below .active').removeClass('active');
                this.$el.find('.tabbable.tabs-below a[href~=#console]').parent().addClass('active');

                this.$el.find('.activePane').removeClass('activePane');
                this.$el.find('#ConsolePane').addClass('activePane');
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
        
        resetAssignment: function () {
            this.setAssignment(this.assignments.current);
            return false;
        },

        startAssignment: function() {
            // TODO STart the timer <Alexender>
            return false;
        },




        // Add a single todo item to the list by creating a view for it, and
        // appending its element to the `<ul>`.
        addOne: function(todo) {
            var view = new TodoView({ model: todo });
            this.$("#AssignmentList ul").append(view.render().el);
        },

        // Add all items in the **Todos** collection at once.
        addAll: function() {
            Todos.each(this.addOne);
        },

        // If you hit return in the main input field, create new **Todo** model,
        // persisting it to *localStorage*.
        createOnEnter: function(e) {
            if (e.keyCode != 13) return;
            if (!this.input.val()) return;

            Todos.create({ title: this.input.val() });
            this.input.val('');
        },

        // Clear all done todo items, destroying their models.
        clearCompleted: function() {
            _.each(Todos.done(), function(todo) { todo.clear(); });
            return false;
        },

        toggleAllComplete: function() {
            var done = this.allCheckbox.checked;
            Todos.each(function(todo) { todo.save({ 'done': done }); });
        }
    });
});