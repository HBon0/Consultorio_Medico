let IndexSchedule = -1;
let IndexSpecialtie = -1;

$("#btnAddSchedule").click(function () {
    IndexSchedule++;
    console.log(IndexSchedule);

    var Values = $("#SlcSchedules option:selected").val();
    alert("Testing");
});