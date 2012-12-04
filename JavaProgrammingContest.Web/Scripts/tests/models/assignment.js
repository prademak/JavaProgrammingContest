// Testable files
jQuery.getScript('/Scripts/application/models/assignment.js');

// Assignment Model Tests
module("Models - Assignments");
test("Model can be initialized without arguments.", function () {
    expect(7);
    var model = new AssignmentModel();
    equal(model.get("Id"),              0,      "initial numeric value correct");
    equal(model.get("Title"),           "",     "initial string value correct");
    equal(model.get("Description"),     "",     "initial string value correct");
    equal(model.get("CodeGiven"),       "",     "initial string value correct");
    equal(model.get("TargetSolveTime"), 0.0,    "initial double value correct");
    equal(model.get("TargetCompileTime"), 0.0,  "initial double value correct");
    equal(model.get("Solved"),          false,  "initial boolean value correct");
});
test("Model can be initialized with arguments and can be modified.", function () {
    expect(3);
    var model = new AssignmentModel({Title: "Test assignment."});
    equal(model.get("Title"), "Test assignment.", "initial string value correct");

    model.set("Title", "Modified title.");
    equal(model.get("Title"), "Modified title.", "modify title string"));

    if (model.get("Description") == "") {
        model.set("Description", "Pindakaas");
        equal(model.get("Description"), "Pindakaas", "string value correct");
    } else {
        fail("initial state incorrect");
    }
});
test("Model can fetch data from server.", function () {
    expect(0);
    var model = new AssignmentModel({ Title: "Get oil change for car." });
    
});
test("Assignments can be submitted.", function () {
    expect(0);
    var model = new AssignmentModel({ Description: "Stop monkeys from throwing their own crap!" });
    model.solve("some code");
});

// Assignment Collection Tests
/*
test("Collection - Can add Model instances as objects and arrays.", function () {
    expect(3);
    var todos = new TodoList();
    equal(todos.length, 0);
    todos.add({ text: "Clean the kitchen" });
    equal(todos.length, 1);
    todos.add([
        { text: "Do the laundry", done: true },
        { text: "Go to the gym" }
    ]);
    equal(todos.length, 3);
});
test("Collection - Can have a url property to define the basic url structure for all contained models.", function () {
    expect(1);
    var todos = new TodoList();
    equal(todos.url, "/todos/");
});
test("Collection - Fires custom named events when the models change.", function () {
    expect(2);
    var todos = new TodoList();
    var addModelCallback = this.spy();
    var removeModelCallback = this.spy();
    todos.bind("add", addModelCallback);
    todos.bind("remove", removeModelCallback);
    // How would you get the "add" event to trigger?
    todos.add({ text: "New todo" });
    ok(addModelCallback.called);
    // How would you get the "remove" callback to trigger?
    todos.remove(todos.last());
    ok(removeModelCallback.called);
});
*/