const text = document.getElementById('text');
const addTaskButton = document.getElementById('add-task-btn');
const saveTaskButton = document.getElementById('save-todo-btn');
const listBox = document.getElementById('listBox');
const saveInd = document.getElementById('saveIndex');

let todoArray = [];

addTaskButton.addEventListener('click', (e) => {
  e.preventDefault();
  let todo = localStorage.getItem('todo');
  if (todo === null) {
    todoArray = [];
  } else {
    todoArray = JSON.parse(todo);
  }
  if (text.value) {
    todoArray.push(text.value);
  }
  text.value = '';
  localStorage.setItem('todo', JSON.stringify(todoArray));
  displayTodo();
});

function displayTodo() {
  let todo = localStorage.getItem('todo');
  if (todo === null) {
    todoArray = [];
  } else {
    todoArray = JSON.parse(todo);
  }
  let htmlCode = '';
  todoArray.forEach((list, ind) => {
    htmlCode += `<div class='flex mb-4 items-center'>
                        <p class='w-full text-grey-darkest'>${list}</p>
                        <button onclick='edit(${ind})' class='flex-no-shrink px-3 py-1 ml-4 mr-2 rounded text-white text-grey bg-[#888]'><i class="far fa-edit"></i></button>
                        <button onclick='deleteTodo(${ind})' class='flex-no-shrink px-3 py-1 ml-2 rounded text-white bg-red-500'><i class="fas fa-minus text-[14px]"></i></button>
                        </div>`;
  });
  listBox.innerHTML = htmlCode;
}

displayTodo();

function deleteTodo(ind) {
  let todo = localStorage.getItem('todo');
  todoArray = JSON.parse(todo);
  todoArray.splice(ind, 1);
  localStorage.setItem('todo', JSON.stringify(todoArray));
  displayTodo();
}

function edit(ind) {
  saveInd.value = ind;
  let todo = localStorage.getItem('todo');
  todoArray = JSON.parse(todo);
  text.value = todoArray[ind];
  addTaskButton.style.display = 'none';
  saveTaskButton.style.display = 'block';
}

saveTaskButton.addEventListener('click', () => {
  let todo = localStorage.getItem('todo');
  todoArray = JSON.parse(todo);
  let id = saveInd.value;
  todoArray[id] = text.value;
  addTaskButton.style.display = 'block';
  saveTaskButton.style.display = 'none';
  text.value = '';
  localStorage.setItem('todo', JSON.stringify(todoArray));
  displayTodo();
});
