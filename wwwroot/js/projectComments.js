function loadComments(projectId) {
    fetch(`/api/ProjectComment/${projectId}`)
        .then(response => response.json())
        .then(data => {
            const list = document.getElementById("comment-list");
            list.innerHTML = "";

            data.forEach(comment => {
                const li = document.createElement("li");
                li.classList.add("list-group-item");
                li.innerHTML = `<strong>${new Date(comment.createdDate).toLocaleString()}</strong><br>${comment.content}`;
                list.appendChild(li);
            });
        });
}

document.getElementById("comment-form")?.addEventListener("submit", function (e) {
    e.preventDefault();
    const content = document.getElementById("comment-content").value;
    const projectId = window.location.href.split("/").pop();

    fetch("/api/ProjectComment", {
        method: "POST",
        headers: {"Content-Type": "application/json"},
        body: JSON.stringify({content: content, projectId: parseInt(projectId)})
    })
        .then(() => {
            document.getElementById("comment-content").value = "";
            loadComments(projectId);
        })
        .catch(err => {
            console.error("POST failed", err);
        });
});
    

