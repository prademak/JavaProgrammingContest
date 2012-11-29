var EditorView;

$(document).ready(function () {
    EditorView = Backbone.View.extend({
        // Representing element
        el: $("#EditorPane"),

        // Default CodeMirror Options
        defaultEditorOptions: {
            mode: "text/x-java",
            theme: "eclipse",
            value: "// No assignment loaded yet.",
    
            lineNumbers: true,
            lineWrapping: false,
            gutter: true,
            fixedGutter: true,

            tabSize: 4,
            indentUnit: 4,
            indentWithTabs: true,
            smartIndent: true,
    
            electricChars: true,
            matchBrackets: true
        },
        
        // CodeMirror Instance
        codeMirror: null,
        
        // 
        currentAssignment: null,

        events: {
            //"keypress #new-todo": "createOnEnter"
        },

        initialize: function () {
            this.codeMirror = new CodeMirror(this.el, this.defaultEditorOptions);
        },
        
        render: function () {
            
        },
        
        setContent: function(content) {
            return this.codeMirror.setValue(content);
        },
        
        getContent: function() {
            return this.codeMirror.getValue();
        },

        setOption: function(option, value) {
            this.codeMirror.setOption(option, value);
        }
    });
});