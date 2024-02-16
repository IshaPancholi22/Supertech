const form = document.getElementById("form");
const username = document.getElementById("userName");
const email = document.getElementById("email");
const companName = document.getElementById("companName");
const phone = document.getElementById("phone");

form.addEventListener("submit", (e) => {
  e.preventDefault();
  checkInputs();
});

function checkInputs() {
  //Get the value the form field.
  const usernameValue = username.value.trim(); //trim to delete blanc space.
  const emailValue = email.value.trim();
  const companName = companName.value.trim();
  const phone = password2.value.trim();

  if (usernameValue === "") {
    setErrorInput(username, "UserName cannot be blanc.");
  } else {
    setSuccessInput(username);
  }

  //###################################
  if (emailValue === "") {
    setErrorInput(email, "Email cannot be blanc.");
  } else {
    validateEmail(emailValue) && setSuccessInput(email);
  }

  //###################################
  if (companName === "") {
    setErrorInput(companName, "Company name is required.");
  } else {
    setSuccessInput(companName);
  }

  //###################################
  if (phone === "") {
    setErrorInput(phone, "Phone number is required");
  } else {
    setSuccessInput(phone);
  }
}

function setErrorInput(input, errorMessage) {
  const formControl = input.parentElement;
  const small = formControl.querySelector("small");

  small.innerText = errorMessage;
  formControl.className = "form__control error";
}

function setSuccessInput(input) {
  const formControl = input.parentElement;
  formControl.className = "form__control success";
}

function validateEmail(email) {
  let regular_expressions =
    /^(([^<>()\[\]\\.,;:\s@"]+(\.[^<>()\[\]\\.,;:\s@"]+)*)|(".+"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/;
  return regular_expressions.test(String(email).toLocaleLowerCase());
}

function validatePassword(password) {
  let regular_expressions = /^(?=.*\d)(?=.*[a-z])(?=.*[A-Z])[0-9a-zA-Z]{8,}$/;
  return regular_expressions.match(regular_expressions);
}
