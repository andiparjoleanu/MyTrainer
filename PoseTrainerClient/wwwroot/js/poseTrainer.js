// Copyright (c) 2019 ml5
//
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

/* ===
ml5 Example
PoseNet using p5.js
=== */
/* eslint-disable */
// Grab elements, create settings, etc.

var pageContent = document.getElementById("pageContent");

var header = document.getElementsByClassName("header")[0];

var video = document.getElementById('video');
video.width = pageContent.offsetWidth;
video.height = pageContent.offsetHeight;


var canvas = document.getElementById('canvas');

canvas.style.top = header.offsetHeight + "px";
canvas.width = pageContent.offsetWidth;
canvas.height = pageContent.offsetHeight;

var ctx = canvas.getContext('2d');
var width = pageContent.offsetWidth;
var height = pageContent.offsetHeight;
ctx.canvas.width = width;
ctx.canvas.height = height;

var myalert = document.getElementById("alert");
var alertMessage = document.getElementById("alertMessage");

window.addEventListener("resize", function () {
    width = pageContent.offsetWidth;
    height = pageContent.offsetHeight;
    
    canvas.width = pageContent.offsetWidth;
    canvas.height = pageContent.offsetHeight;
    ctx.canvas.width = pageContent.offsetWidth;
    ctx.canvas.height = pageContent.offsetHeight;
    video.width = pageContent.offsetWidth;
    video.height = pageContent.offsetHeight;
});

// The detected positions will be inside an array
let poses = [];

// Create a webcam capture
if (navigator.mediaDevices && navigator.mediaDevices.getUserMedia) {
    navigator.mediaDevices.getUserMedia({ video: true }).then(function (stream) {
        video.srcObject = stream;
        video.play();
    });
}

// A function to draw the video and poses into the canvas.
// This function is independent of the result of posenet
// This way the video will not seem slow if poseNet 
// is not detecting a position
function drawCameraIntoCanvas() {
    // Draw the video element into the canvas
    ctx.drawImage(video, 0, 0, pageContent.offsetWidth, pageContent.offsetHeight);
    ctx.fillStyle = '#000000';
    ctx.fillRect(0, 0, pageContent.offsetWidth, pageContent.offsetHeight);
    ctx.lineWidth = 5;
    ctx.fillStyle = '#00FFFF';
    ctx.strokeStyle = '#00FFFF';

    // We can call both functions to draw all keypoints and the skeletons
    drawKeypoints();
    drawSkeleton();
    window.requestAnimationFrame(drawCameraIntoCanvas);
}
// Loop over the drawCameraIntoCanvas function
drawCameraIntoCanvas();

// Create a new poseNet method with a single detection
const poseNet = ml5.poseNet(video, modelReady);
poseNet.on('pose', gotPoses);

// A function that gets called every time there's an update from the model
function gotPoses(results) {
    poses = results;
}

function modelReady() {
    console.log("model ready");
    poseNet.multiPose(video)
}

var countStarted = false;

class Status {
    constructor(repStarted, goalAchieved, reps) {
        this.repStarted = repStarted;
        this.goalAchieved = goalAchieved;
        this.reps = reps;
        this.moved = false;
    }
}

var leftStatus = new Status(false, false, 0);
var rightStatus = new Status(false, false, 0);

// A function to draw ellipses over the detected keypoints
function drawKeypoints() {
    // Loop through all the poses detected
    if (poses[0] != undefined) {
        let currentPose = poses[0];
        // For each pose detected, loop through all the keypoints
        for (let j = 0; j < currentPose.pose.keypoints.length; j++) {
            let keypoint = currentPose.pose.keypoints[j];
            // Only draw an ellipse if the pose probability is bigger than 0.2
            if (keypoint.score > 0.2) {
                ctx.beginPath();
                ctx.arc(keypoint.position.x, keypoint.position.y, 5, 0, 2 * Math.PI);
                //console.log("(" + keypoint.position.x + ", " + keypoint.position.y + ")");
                ctx.fill();
                ctx.stroke();
            }
        }

        if (countStarted) {
            evaluate(currentPose);
        }

    }
}

var exercise;

window.onload = function () {
    var exerciseDetails = document.getElementById("exercise");
    var startButton = document.getElementById("start");
    var details = document.getElementById("details");
    var selectExercise = document.getElementById("selectExercise");

    startButton.onclick = function () {
        exerciseDetails.removeChild(startButton);
        exerciseDetails.removeChild(details);

        exercise = document.createElement("script");
        exercise.setAttribute('src', selectExercise.value);
        document.body.appendChild(exercise);

        var exerciseDetailsCounters = document.createElement("div");
        exerciseDetailsCounters.id = "counters";
        exerciseDetailsCounters.classList.add("d-flex");
        exerciseDetailsCounters.classList.add("flex-row");
        exerciseDetailsCounters.classList.add("align-items-center");
        exerciseDetailsCounters.classList.add("justify-content-between");
        exerciseDetailsCounters.style.width = "66%";

        var countLeftContainer = document.createElement("div");
        countLeftContainer.style.display = "flex";
        countLeftContainer.style.flexDirection = "column";
        countLeftContainer.style.justifyContent = "center";
        countLeftContainer.style.alignItems = "center";
        countLeftContainer.classList.add("mr-2");


        var countLeftLabel = document.createElement("p");
        countLeftLabel.innerHTML = "Repetări stânga";
        countLeftLabel.style.textAlign = "center";
        countLeftLabel.style.color = "white";
        countLeftLabel.style.fontSize = "18px";
        countLeftContainer.appendChild(countLeftLabel);

        var counterLeft = document.createElement("p");
        counterLeft.id = "counterLeft";
        counterLeft.innerHTML = "0";
        counterLeft.style.color = "cyan";
        counterLeft.style.fontSize = "25px";
        countLeftContainer.appendChild(counterLeft);

        exerciseDetailsCounters.appendChild(countLeftContainer);

        var countRightContainer = document.createElement("div");
        countRightContainer.style.display = "flex";
        countRightContainer.style.flexDirection = "column";
        countRightContainer.style.justifyContent = "center";
        countRightContainer.style.alignItems = "center";
        countRightContainer.classList.add("ml-2");

        var countRightLabel = document.createElement("p");
        countRightLabel.innerHTML = "Repetări dreapta";
        countRightLabel.style.textAlign = "center";
        countRightLabel.style.color = "white";
        countRightLabel.style.fontSize = "18px";
        countRightContainer.appendChild(countRightLabel);

        var counterRight = document.createElement("p");
        counterRight.id = "counterRight";
        counterRight.innerHTML = "0";
        counterRight.style.color = "cyan";
        counterRight.style.fontSize = "25px";
        countRightContainer.appendChild(counterRight);

        exerciseDetailsCounters.appendChild(countRightContainer);

        exerciseDetails.appendChild(exerciseDetailsCounters);

        var stopButton = document.createElement("button");
        exerciseDetails.appendChild(stopButton);
        stopButton.classList.add("btn");
        stopButton.classList.add("start");
        stopButton.innerHTML = "Finalizează";
        stopButton.onclick = function () {
            exerciseDetails.removeChild(stopButton);
            exerciseDetails.removeChild(exerciseDetailsCounters);
            exerciseDetails.appendChild(details);
            exerciseDetails.appendChild(startButton);

            leftStatus = new Status(false, false, 0);
            rightStatus = new Status(false, false, 0);

            document.body.removeChild(exercise);
            evaluate = function () { };
            countStarted = false;
        }

        countStarted = true;
        //side = selectPart.value;
    };
}

function dotProduct(vector1, vector2) {
    var result = vector1.x * vector2.x + vector1.y * vector2.y;

    if (result > 1.0) {
        return 1.0;
    }
    else if (result < -1.0) {
        return -1.0;
    }

    return result;
}

// A function to draw the skeletons
function drawSkeleton() {
    // Loop through all the skeletons detected
    for (let i = 0; i < poses.length; i++) {
        // For every skeleton, loop through all body connections
        for (let j = 0; j < poses[i].skeleton.length; j++) {
            let partA = poses[i].skeleton[j][0];
            let partB = poses[i].skeleton[j][1];
            ctx.beginPath();
            ctx.moveTo(partA.position.x, partA.position.y);
            ctx.lineTo(partB.position.x, partB.position.y);
            ctx.stroke();
        }
    }
}

function evaluate() {}