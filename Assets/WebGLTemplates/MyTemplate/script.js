UpdateGameItems: function () {
  const scrollList = document.getElementById("scrollList");
  scrollList.innerHTML = "";

  const gameItems = window.gameItems;

  gameItems.forEach((data) => {
    const itemDiv = document.createElement("div");
    itemDiv.className = "scroll-item";
    itemDiv.id = data.sceneName;

    const img = document.createElement("img");
    img.src = data.imgPath;
    img.alt = "item";

    itemDiv.appendChild(img);
    scrollList.appendChild(itemDiv);

    // ✅ クリック判定
    itemDiv.addEventListener("click", () => {
      SendMessage("WebBridge", "SelectItem", data.sceneName );
    });
  });

  scrollList.style.transformOrigin = "center center";
}