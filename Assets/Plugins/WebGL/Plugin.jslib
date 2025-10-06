
mergeInto(LibraryManager.library, {
  ShowAlert: function (msgPtr) {
    const msg = UTF8ToString(msgPtr);
    alert("Unity says: " + msg);
  },

  LogToConsole: function (msgPtr) {
    const msg = UTF8ToString(msgPtr);
    console.log("[From Unity]:", msg);
  },

  SetSceneName: function (sceneNamePtr, imgPathPtr, titlePtr, summaryPtr) {
    const sceneName = UTF8ToString(sceneNamePtr);
    const imgPath = 'images/thumb/' + UTF8ToString(imgPathPtr);
    const title = UTF8ToString(titlePtr);
    const summary = UTF8ToString(summaryPtr);
    const item = {
        title,
        sceneName,
        imgPath,
        summary
    };
    if (window.cardItems) {
        window.cardItems.push(item);
    }
    else {
        window.cardItems = [item];
    }
  },
  
  UpdateContent: function (now) {
    const activeSceneName = UTF8ToString(now);
    const gameTitle = document.getElementById("game-title");
    const gameSummary = document.getElementById("game-summary");
    const container = document.getElementById("rightColumn");

    const items = window.cardItems;
    if (!items) return;

    container.innerHTML = "";

    items.forEach((item) => {
      if (item.sceneName == activeSceneName) {
        gameTitle.textContent = item.title;
        gameSummary.innerHTML = item.summary;
        return; 
      }
      const card = document.createElement("div");
      card.className = "item-card";

      card.innerHTML = `
        <img class="item-thumb" src="${item.imgPath}" alt="${item.title}">
        <div class="item-info">
          <div class="item-title">${item.title}</div>
          <div class="item-desc">${item.sceneName}</div>
        </div>
    ` ;

      container.appendChild(card);

      card.addEventListener("click", () => {
        SendMessage("WebBridge", "LoadScene", item.sceneName);
      })
    });
  },

  AddGameItem: function (sceneNamePtr) {
    const sceneName = UTF8ToString(sceneNamePtr);
    const item = {
        sceneName,
        imgPath: "images/item/item.png"
    };
    if (window.gameItems) {
        window.gameItems.push(item);
    }
    else {
        window.gameItems = [item];
    }
  },
  
  UpdateGameItems: function () {
    const scrollList = document.getElementById("scrollList");
    scrollList.innerHTML = "";

    const gameItems = window.gameItems;
    window.gameItems = null;

    gameItems.forEach((data) => {
      const itemDiv = document.createElement("div");
      itemDiv.className = "scroll-item";
      itemDiv.id = data.sceneName;

      const img = document.createElement("img");
      img.src = data.imgPath;
      img.alt = "item";

      itemDiv.appendChild(img);
      scrollList.appendChild(itemDiv);
    });


    const buttons = scrollList.querySelectorAll(".scroll-item");
    buttons.forEach((btn, index) => {
      btn.addEventListener("click", () => {
        if (btn.classList.contains("selected")) {
          btn.classList.remove("selected")
          SendMessage("WebBridge", "UnSelectItem", gameItems[index].sceneName);
          return;
        }
        buttons.forEach(b => b.classList.remove("selected"));

        btn.classList.add("selected");

        SendMessage("WebBridge", "SelectItem", gameItems[index].sceneName);
      });
    });

    scrollList.style.transformOrigin = "center center";
  },


  UpdateItemIcon: function (sceneNamePtr, fileNamePtr) {
    const sceneName = UTF8ToString(sceneNamePtr);
    const fileNamePath = UTF8ToString(fileNamePtr);
    const item = document.getElementById(sceneName);
    const img = item.querySelector("img");
    img.src = "images/item/" + fileNamePath;
  },

  UpdateItemText: function(itemNamePtr, itemIdPtr, sceneNamePtr) {
    const itemName = UTF8ToString(itemNamePtr);
    const itemId = UTF8ToString(itemIdPtr);
    const sceneName = UTF8ToString(sceneNamePtr);
    const gameSummary = document.getElementById("game-summary");

    if (document.getElementById(itemId)) {
      return;
    }

    const walker = document.createTreeWalker(
      gameSummary,
      NodeFilter.SHOW_TEXT,
      null,
      false
    );

    let targetNode = null;
    while (walker.nextNode()) {
      const node = walker.currentNode;
      if (node.nodeValue.includes(itemName)) {
        targetNode = node;
        break;
      }
    }

    if (!targetNode) {
      return;
    }

    const parts = targetNode.nodeValue.split(itemName);
    const beforeText = document.createTextNode(parts[0]);
    const afterText = document.createTextNode(parts[1]);

    const button = document.createElement("button");
    button.id = itemId;
    button.className = "word-button";
    button.textContent = itemName;
    button.addEventListener("click", () => {
      SendMessage("WebBridge", "SetGameItem", `${itemId}+${sceneName}`);
    });

    const parent = targetNode.parentNode;
    parent.replaceChild(afterText, targetNode);
    parent.insertBefore(button, afterText);
    parent.insertBefore(beforeText, button);
  },

  DisabledItemButton: function (sceneNamePtr) {
    const sceneName = UTF8ToString(sceneNamePtr);

    const button = document.getElementById(sceneName);
    button.querySelector("img").src = "images/item/item.png";
    button.classList.remove("selected");
  },

  UnSelectedItemButtons: function () {
    const buttons = scrollList.querySelectorAll(".scroll-item");
    buttons.forEach(b => b.classList.remove("selected"));
  }
});
