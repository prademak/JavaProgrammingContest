var AssignmentView;

$(document).ready(function () {
    /**
     * View for the Assignment list
     */
    AssignmentView = Backbone.View.extend({
        // Element for this view
        el: $('#AssignmentList ul'),

        // List tag.
        tagName: "li",
        
        modelIndex: 0,
        current: null,
        started: false,

        // Cache the template function for a single item.
        template: $('#assignment-template').html(),

        // The DOM events specific to this view.
        events: {
            "click li": "select"
        },

        // The TodoView listens for changes to its model, re-rendering. Since there's
        // a one-to-one correspondence between a **Todo** and a **TodoView** in this
        // app, we set a direct reference on the model for convenience.
        initialize: function () {
            this.model.bind('reset', this.render, this);
            this.model.bind('change', this.render, this);
            this.model.bind('destroy', this.remove, this);
        },

        // Re-render the titles of the todo item.
        render: function () {
            // Check if model is empty
            if (this.model.length == 0) {
                noty({ text: '<h3>No assignments found!</h3>', type: 'warning', layout: 'topCenter', modal: true, closeWith: [] });
                return false;
            }

            // Check for current
            if (this.current == null) {
                this.current = this.model.at(0);
                this.trigger('select', this.current);
            }
            //app.setAssignment(this.current);
            this.renderPane();
            
            // Template
            var list = "";
            var ns = this;
            this.model.each(function (model) {
                var currTmpl = ""+ns.template;
                $.each(model.attributes, function (k, v) {
                    currTmpl = currTmpl.replace('{'+k+'}', v);
                });
                list += currTmpl;
            });
            this.$el.html(list);
            
            // Set selected
            this.$el.find('.selected').removeClass('selected');
            this.$el.find('li:eq('+this.modelIndex+')').addClass('selected');

            return this;
        },
        
        nextAssignment: function () {
            /*var cur = this.current;
            var newkey = 0;
            this.model.each(function (value, key) {
                if (value == cur) {
                    newkey = key + 1;
                }
            });*/
            this.current = this.model.at(++this.modelIndex);
            this.trigger('select', this.current);
            this.render();
        },

        renderPane: function() {
            var ass = $('#AssignmentPane');
            ass.find('h1').text(this.current.get('Title'));
            ass.find('.description').text(this.current.get('Description'));
            ass.find('.time').text((this.current.get('MaxSolveTime')/60)+' minutes');
        },

        select: function (e) {
            // Check if an assignment is in progress
            if (this.started == true) return false;
            
            this.$el.find('.selected').removeClass('selected');
            $(e.currentTarget).addClass('selected');
            this.setState($(e.currentTarget), 'selected');
            
            //alert('selected'); //this.setActive();
            //window.app.loadAssignment(this);
        },
        
        // Toggle the `"done"` state of the model.
        // selected normal inactive(done)
        setState: function (el, state) {
            if (state == 'selected') {
                this.$el.find('.selected').removeClass('selected');
                el.addClass('selected');
                var mdl = this.model.where({ Id: parseInt(el.attr('data-assignment')) })[0];
                this.current = mdl;
                
                // Fire event
                this.trigger('select', mdl);
            }
        }
    });
});