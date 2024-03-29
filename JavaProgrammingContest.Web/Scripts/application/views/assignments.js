﻿var AssignmentView;

$(document).ready(function () {
    /**
     * View for the Assignment list
     */
    AssignmentView = Backbone.View.extend({
        // Element for this view
        el: $('#AssignmentList ul'),

        // List tag.
        tagName: "li",
        
        // Keeps track of the current assignment
        modelIndex: 0,
        current: null,
        started: false,
        startTime: null,

        // Cache the template function for a single item.
        template: $('#assignment-template').html(),

        // The DOM events specific to this view.
        events: {
            "click li": "select"
        },

        initialize: function () {
            this.model.bind('reset', this.render, this);
            this.model.bind('change', this.render, this);
            this.model.bind('destroy', this.remove, this);
        },
        render: function () {
            var firstRenderer = (this.current == null);

            // Check if model is empty
            if (this.model.length == 0) {
                noty({ text: '<h3>Geen opdrachten gevonden!</h3>', type: 'warning', layout: 'topCenter', modal: true, closeWith: [] });
                return false;
            }

            // Check for current
            if (this.current == null) {
                for (var i = 0; i < this.model.length; i++) {
                    this.modelIndex = i;
                    this.current = this.model.at(i);
                    if (this.current.attributes.HasBeenSubmitted == false)
                        break;
                }

                if (this.current.attributes.HasBeenSubmitted == true) {
                    noty({ text: '<h3>Je hebt alle opdrachten afgerond!</h3><strong>Je kunt nu verder gaan naar het <a href="/Scores">scorebord</a>.</strong>', type: 'warning', layout: 'topCenter', modal: true, closeWith: [] });
                }

                var ns = this;
                API.Progress.inProgress(this.current.id, function(data) {
                    if (data.StartTime) {
                        var modalProgress = $('#assignmentInProgressModal');
                        modalProgress.find('.moment').text(moment(data.StartTime).fromNow());
                        modalProgress.find('#stopProgress').click(function () {
                            modalProgress.modal('hide');
                            API.Progress.stop();
                            //ns.$el.find('li[data-assignment=' + ns.modelIndex + ']').addClass('done');
                            //ns.nextAssignment();
                            ns.trigger('next');
                        });  
                        modalProgress.modal({
                            backdrop: true,
                            keyboard: false,
                            show: true
                        });
                    }
                }); // Check if the assignment is already in progress
                this.trigger('select', this.current);
            }
            this.renderPane();
            
            // Template
            if (firstRenderer) {
                var list = "";
                var ns = this;
                this.model.each(function (model, key) {
                    var currTmpl = "" + ns.template;
                    currTmpl = currTmpl.replace('{Class}', model.attributes.HasBeenSubmitted == true ? 'done' : '');
                    currTmpl = currTmpl.replace('{dataKey}', key); // Replace the data-attr value
                    $.each(model.attributes, function (k, v) {
                        currTmpl = currTmpl.replace('{' + k + '}', v);
                    });
                    list += currTmpl;
                });
                this.$el.html(list);
            }
            
            // Set selected
            this.$el.find('.selected').removeClass('selected');
            this.$el.find('li:eq('+this.modelIndex+')').addClass('selected');

            return this;
        },
        
        nextAssignment: function () {
            if (this.modelIndex+1 < this.model.length) {
                this.setAssignment(++this.modelIndex);
                return false;
            } else {
                noty({ text: '<h3>Je hebt alle opdrachten afgerond!</h3><strong>Je kunt nu verder gaan naar het <a href="/Scores">scorebord</a>.</strong>', type: 'warning', layout: 'topCenter', modal: true, closeWith: [] });
                return false;
            }
        },
        
        setAssignment: function (id) {
            // Set the current model id
            this.modelIndex = id;

            // Set current element
            this.current = this.model.at(id);
            
            // Fire event
            this.trigger('select', this.current);
            
            // Render the new list
            this.render();

            // Check if the assignment is already in progress
            var ns = this;
            API.Progress.inProgress(this.current.id, function (data) {
                if (data.StartTime) {
                    var modalProgress = $('#assignmentInProgressModal');
                    modalProgress.find('.moment').text(moment(data.StartTime).fromNow());
                    modalProgress.find('#stopProgress').click(function () {
                        modalProgress.modal('hide');
                        API.Progress.stop();
                        //ns.$el.find('li[data-assignment=' + ns.modelIndex + ']').addClass('done');
                        //ns.nextAssignment();
                        ns.trigger('next');
                    });
                    modalProgress.modal({
                        backdrop: true,
                        keyboard: false,
                        show: true
                    });
                }
            });
        },

        renderPane: function () {
            var ass = $('#AssignmentPane');
            ass.find('h1').text(this.current.get('Title'));
            ass.find('.description').text(this.current.get('Description'));
            ass.find('.time').text(Math.round(this.current.get('MaxSolveTime') / 60) + ' minutes');

            ass = $('#StartPane');
            ass.find('h1').text("Opdracht " + this.current.get('Id'));
            ass.find('.description').text("Zodra de tijds is gestart, mag de opdracht worden gelezen en kan de opdracht worden gemaakt.");
            ass.find('.time').text(Math.round(this.current.get('MaxSolveTime') / 60) + ' minutes');
        },

        // Click Event
        select: function (e) {
            // Check if an assignment is in progress
            if (this.started == true) return false;
            if ($(e.currentTarget).hasClass('done')) return false;

            this.setAssignment(parseInt($(e.currentTarget).attr('data-assignment')));
        }
    });
});