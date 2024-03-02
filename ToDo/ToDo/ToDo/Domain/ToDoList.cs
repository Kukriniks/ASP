using ToDo.ToDo.Models;
namespace ToDo.ToDo.Domain
{
    public class ToDoList
    {  
        public List<IToDoNode>? ToDoNodeList { get; set; }

        public ToDoList()
        {
            ToDoNodeList =
			[
				new ToDoNode(id: 1, label: "someTest", _IsDone: false, createdDate: DateTime.UtcNow, updatedDate: DateTime.UtcNow, ownerId:1),
				new ToDoNode(id: 2, label: "someTest", _IsDone: false, createdDate: DateTime.UtcNow, updatedDate: DateTime.UtcNow, ownerId:2),
				new ToDoNode(id: 3, label: "someTest", _IsDone: false, createdDate: DateTime.UtcNow, updatedDate: DateTime.UtcNow, ownerId:3),
				new ToDoNode(id: 4, label: "someTest", _IsDone: false, createdDate: DateTime.UtcNow, updatedDate: DateTime.UtcNow, ownerId:4),
				new ToDoNode(id: 5, label: "someTest", _IsDone: false, createdDate: DateTime.UtcNow, updatedDate: DateTime.UtcNow, ownerId:1),
				new ToDoNode(id: 6, label: "someTest", _IsDone: false, createdDate: DateTime.UtcNow, updatedDate: DateTime.UtcNow, ownerId:2),
				new ToDoNode(id: 7, label: "someTest", _IsDone: false, createdDate: DateTime.UtcNow, updatedDate: DateTime.UtcNow, ownerId:3),
				new ToDoNode(id: 8, label: "someTest", _IsDone: false, createdDate: DateTime.UtcNow, updatedDate: DateTime.UtcNow, ownerId:4),
				new ToDoNode(id: 9, label: "someTest", _IsDone: false, createdDate: DateTime.UtcNow, updatedDate: DateTime.UtcNow, ownerId:1),
				new ToDoNode(id: 10, label: "someTest", _IsDone: false, createdDate: DateTime.UtcNow, updatedDate: DateTime.UtcNow, ownerId:2),
				new ToDoNode(id: 11, label: "someTest", _IsDone: false, createdDate: DateTime.UtcNow, updatedDate: DateTime.UtcNow, ownerId:3),
				new ToDoNode(id: 12, label: "someTest", _IsDone: false, createdDate: DateTime.UtcNow, updatedDate: DateTime.UtcNow, ownerId:4),
				new ToDoNode(id: 13, label: "someTest", _IsDone: false, createdDate: DateTime.UtcNow, updatedDate: DateTime.UtcNow, ownerId:1),
				new ToDoNode(id: 14, label: "someTest", _IsDone: false, createdDate: DateTime.UtcNow, updatedDate: DateTime.UtcNow, ownerId:2),
				new ToDoNode(id: 15, label: "someTest", _IsDone: false, createdDate: DateTime.UtcNow, updatedDate: DateTime.UtcNow, ownerId:3),
				new ToDoNode(id: 16, label: "someTest", _IsDone: false, createdDate: DateTime.UtcNow, updatedDate: DateTime.UtcNow, ownerId:4),
				new ToDoNode(id: 17, label: "someTest", _IsDone: false, createdDate: DateTime.UtcNow, updatedDate: DateTime.UtcNow, ownerId:1),
				new ToDoNode(id: 18, label: "someTest", _IsDone: false, createdDate: DateTime.UtcNow, updatedDate: DateTime.UtcNow, ownerId:2),
				new ToDoNode(id: 19, label: "someTest", _IsDone: false, createdDate: DateTime.UtcNow, updatedDate: DateTime.UtcNow, ownerId:4),
				new ToDoNode(id: 20, label: "someTest", _IsDone: false, createdDate: DateTime.UtcNow, updatedDate: DateTime.UtcNow, ownerId:4),
			];
        }
    }
}
