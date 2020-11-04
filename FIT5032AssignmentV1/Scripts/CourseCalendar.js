var xmlHttp = new XMLHttpRequest();
xmlHttp.open("GET", "ProviderCourses/findAll", false);
xmlHttp.send();
var courses = JSON.parse(xmlHttp.responseText);
//findEvent();
//console.log(courses);


function findEvent() {
    var events = [];
    $.each(courses, function (i, data) {
        //console.log(data);
        events.push(
            {
                eventId: data.ProviderCourseId,
                title: data.CourseName,
                description: data.CourseType.CourseTypeDec,
                start: moment(data.CourseTime),
                backgroundColor: "#9501fc",
                borderColor: "#fc0101"
            });
    });
    generateCourseCalendar(events)
}
 

function generateCourseCalendar(events) {
    $('#calendar').fullCalendar({
        header:
        {
            left: 'prev,next today',
            center: 'title',
            right: 'month,agendaWeek,agendaDay'
        },
        buttonText: {
            today: 'today',
            month: 'month',
            week: 'week',
            day: 'day'
        },
        contentHeight: 600,
        businessHours: true,
        events: events,
        eventClick: function (event) {
            //console.log(event);
            var txt;
            console.log(moment(event.CourseTime).format("DD-MM-YYYY HH:mm"));
            var r = confirm("Do You Want To Book Course Which Name is:  " + event.title + " on:  " + moment(event.start).format("DD/MM/YYYY HH:mm")+ "  ?");
            if (r == true) {
                if (event.start > $.now()) {
                    var url = "/BookCourse/Book/" + event.eventId;
                    console.log(url);
                    window.open(url, "Thanks for Booking!");
                } else {
                    alert('You cannot book previous course');
                }
                
            } else {
                txt = "You pressed Cancel!";
            }
        }
    });

}

