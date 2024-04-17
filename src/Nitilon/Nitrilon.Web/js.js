var RedSmiley = document.querySelector("#redSmiley");
var YellowSmiley = document.querySelector("#yellowSmiley");
var GreenSmiley = document.querySelector("#greenSmiley");
let Event = 1;
let APIUrl = 'https://localhost:7049/api/EventRating';
let rating1 = {
         method: 'POST',
         headers: {
            'Content-Type': 'application/json'
         },
         body: JSON.stringify({ EventId: Event,  RatingId: 1 })
}
      let rating2 = {
         method: 'POST',
         headers: {
            'Content-Type': 'application/json'
         },
         body: JSON.stringify({ EventId: Event,  RatingId: 2 })
}
      let rating3 = {
         method: 'POST',
         headers: {
            'Content-Type': 'application/json'
         },
         body: JSON.stringify({ EventId: Event,  RatingId: 3 })
      }
RedSmiley.addEventListener("click", function()  {
sendToServer(1)
});

YellowSmiley.addEventListener("click", function() {
sendToServer(2)
});

GreenSmiley.addEventListener("click", function() 
{
sendToServer(3)
});

function sendToServer(e) {
   
   if (e === 1) {
      // Send the rating to the server
      fetch(APIUrl, rating1)
      
         .then(response => {
            if (response.ok) {
               console.log('Rating sent successfully');
            } else {
               console.error('Failed to send rating');
            }
         })
         .catch(error => {
            console.error('Error sending rating:', error);
         });
   }
   else if (e === 2) {
  fetch(APIUrl, rating2)
      
         .then(response => {
            if (response.ok) {
               console.log('Rating sent successfully');
            } else {
               console.error('Failed to send rating');
            }
         })
         .catch(error => {
            console.error('Error sending rating:', error);
         });

   }
   else if (e === 3){
        fetch(APIUrl, rating3)
      
         .then(response => {
            if (response.ok) {
               console.log('Rating sent successfully');
            } else {
               console.error('Failed to send rating');
            }
         })
         .catch(error => {
            console.error('Error sending rating:', error);
         });
   }
   }

