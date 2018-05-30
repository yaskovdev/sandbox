var button = document.getElementById("myBtn");
button.addEventListener("click", myFunction);

function myFunction() {
  var v1 = document.getElementById('n1').value;
  var v2 = document.getElementById('n2').value;
  var str = {"value1":  v1 , "value2":v2};
  var xmlhttp = new XMLHttpRequest();
  xmlhttp.open("POST", context + "/addNumber", true);
  xmlhttp.setRequestHeader("Content-Type", "application/json");
  xmlhttp.send(JSON.stringify(str));
  xmlhttp.onreadystatechange = function() {
    console.log(xmlhttp.response);
  }
}