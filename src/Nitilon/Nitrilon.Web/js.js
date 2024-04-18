var RedSmiley = document.querySelector("#redSmiley");
var YellowSmiley = document.querySelector("#yellowSmiley");
var GreenSmiley = document.querySelector("#greenSmiley");
let Event = 1;
let APIUrl = "https://localhost:7049/api/EventRating";
let feedback = document.querySelector(".feedback");
let ratings = document.querySelector("#ratings");

function showFeedback() {
  console.dir(feedback);
  feedback.classList.add("show");
  ratings.classList.add("hidden");

  setTimeout(function () {
    feedback.classList.remove("show");
    ratings.classList.remove("hidden");
  }, 3000);
}

RedSmiley.addEventListener("click", function () {
  sendToServer(1);
});

YellowSmiley.addEventListener("click", function () {
  sendToServer(2);
});

GreenSmiley.addEventListener("click", function () {
  sendToServer(3);
});

function sendToServer(e) {
  let rating = {
    method: "POST",
    headers: {
      "Content-Type": "application/json",
    },
    body: JSON.stringify({ EventId: Event, RatingId: e }),
  };

  // Send the rating to the server
  fetch(APIUrl, rating)
    .then((response) => {
      if (response.ok) {
        console.log("Rating sent successfully");
      } else {
        console.error("Failed to send rating");
      }
    })
    .catch((error) => {
      console.error("Error sending rating:", error);
    });
  showFeedback();
}
