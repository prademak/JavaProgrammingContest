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
        
        current: null,

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
            console.warn('Rendering assignments..');
            // Fetch the data
            /*var ns = this;
            this.model.fetch({
                url: '/api/Assignments',
                success: function () {
                    if (ns.model.length <= 0) {
                        // TODO: Use noty for this.
                        $('#noAssignmentsModal').modal({
                            backdrop: true,
                            keyboard: false,
                            show: true
                        });
                    } else {
                        ns.$el.html(ns.template(ns.model.toJSON()));
                        //console.log(ns.model.toJSON());
                    }
                },
                
                error: function () {
                    // TODO: Use noty for this
                    $('#noAssignmentsModal').modal({
                        backdrop: true,
                        keyboard: false,
                        show: true
                    });
                }
            });*/


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
            return this;
        },
        
        select: function(e) {
            $(e.currentTarget).addClass('selected');
            this.setState($(e.currentTarget),'selected');
            //alert('selected'); //this.setActive();
            //window.app.loadAssignment(this);
        },
        
        // Toggle the `"done"` state of the model.
        // selected normal inactive(done)
        setState: function (el, state) {
            if (state == 'selected') {
                el.addClass('selected');
                var mdl = this.model.where({ Id: parseInt(el.attr('data-assignment')) })[0];
                this.current = mdl;
                
                // Fire event
                this.trigger('select', mdl);
            }
        }
    });
});