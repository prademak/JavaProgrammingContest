var AssignmentView;

$(document).ready(function () {
    /**
     * View for the Assignment list
     */
    AssignmentView = Backbone.View.extend({
        // List tag.
        tagName: "li",

        // Cache the template function for a single item.
        template: _.template($('#assignment-template').html()),

        // The DOM events specific to this view.
        events: {
            "click": "select"
        },

        // The TodoView listens for changes to its model, re-rendering. Since there's
        // a one-to-one correspondence between a **Todo** and a **TodoView** in this
        // app, we set a direct reference on the model for convenience.
        initialize: function() {
            this.model.bind('change', this.render, this);
            this.model.bind('destroy', this.remove, this);
        },

        // Re-render the titles of the todo item.
        render: function() {
            //this.$el.html(this.template(this.model.toJSON()));
            this.$el.html(this.template({title: 'testjuhh'}));
            this.$el.toggleClass('done', this.model.get('done'));
            this.input = this.$('.edit');
            return this;
        },
        
        select: function() {
            alert('selected'); //this.setActive();
            //window.app.loadAssignment(this);
        },
        
        // Toggle the `"done"` state of the model.
        setActive: function() {
            this.model.toggle();
        },

        setInactive: function() {
            
        },

        // Remove the item, destroy the model.
        clear: function() {
            this.model.clear();
        }
    });
});