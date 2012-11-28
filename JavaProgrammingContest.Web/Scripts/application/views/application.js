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
            "click .tabbable.tabs-below li a": "changeViewEvent"
            //"keypress #new-todo": "createOnEnter",
            //"click #clear-completed": "clearCompleted",
            //"click #toggle-all": "toggleAllComplete"
        },

        initialize: function () {
            this.console = new ConsoleView();
            this.editor = new EditorView();
            this.assignments = new AssignmentView({ model: new AssignmentCollection() });
            //asCol.reset([{ Id: 0, Title: 'asdf' }, { Id: 0, Title: 'asdfff' }]);
            
            
            //this.input = this.$("#new-todo");
            //this.allCheckbox = this.$("#toggle-all")[0];

            //Todos.bind('add', this.addOne, this);
            //Todos.bind('reset', this.addAll, this);
            //Todos.bind('all', this.render, this);

            //this.footer = this.$('footer');
            //this.main = $('#main');

            //Todos.fetch();
        },

        // Re-rendering the App just means refreshing the statistics -- the rest
        // of the app doesn't change.
        render: function () {
            // Firstly check the assignments
            this.assignments.render();
            /*if (this.assignments.length <= 0) {
                this.$('#noAssignmentsModal').modal({
                    keyboard: false,
                    backdrop: true,
                    show: true
                });
                console.log('Lawl');
            } else {
                console.log(this.assignments.length);
            }*/


            /*var done = Todos.done().length;
            var remaining = Todos.remaining().length;

            if (Todos.length) {
                this.main.show();
                this.footer.show();
                this.footer.html(this.statsTemplate({ done: done, remaining: remaining }));
            } else {
                this.main.hide();
                this.footer.hide();
            }

            this.allCheckbox.checked = !remaining;*/
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
            } else {
                console.warn('Unknown view type "'+view+'".');
            }
        },
        
        changeViewEvent: function(e) {
            this.changeView($(e.currentTarget).attr('href').substring(1));
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