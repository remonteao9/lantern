document.addEventListener("DOMContentLoaded", () => {
  const container = document.getElementById("rightColumn");

  // 例：動的に並べたいデータ（本番はAPIなどから取得してもOK）
  const items = [
    {
      title: "ステージ1",
      desc: "最初のステージ。操作を覚えよう。",
      thumb: "images/sc.jpg",
    },
    {
      title: "ステージ2",
      desc: "敵が出現。攻撃と防御を試そう。",
      thumb: "images/sc.jpg",
    },
    {
      title: "ステージ3",
      desc: "ボス戦！勝てば次の章へ。",
      thumb: "images/sc.jpg",
    },
    {
      title: "ステージ1",
      desc: "最初のステージ。操作を覚えよう。",
      thumb: "images/sc.jpg",
    },
    {
      title: "ステージ2",
      desc: "敵が出現。攻撃と防御を試そう。",
      thumb: "images/sc.jpg",
    },
    {
      title: "ステージ3",
      desc: "ボス戦！勝てば次の章へ。",
      thumb: "images/sc.jpg",
    },
    {
      title: "ステージ1",
      desc: "最初のステージ。操作を覚えよう。",
      thumb: "images/sc.jpg",
    },
    {
      title: "ステージ2",
      desc: "敵が出現。攻撃と防御を試そう。",
      thumb: "images/sc.jpg",
    },
    {
      title: "ステージ3",
      desc: "ボス戦！勝てば次の章へ。",
      thumb: "images/sc.jpg",
    },
  ];

  // 要素を生成して挿入
  items.forEach((item) => {
    const card = document.createElement("div");
    card.className = "item-card";

    card.innerHTML = `
      <img class="item-thumb" src="${item.thumb}" alt="${item.title}">
      <div class="item-info">
        <div class="item-title">${item.title}</div>
        <div class="item-desc">${item.desc}</div>
      </div>
    `;

    container.appendChild(card);
  });

  const scrollList = document.getElementById("scrollList");

  // ✅ 表示したい画像のリスト
  const images = [
    "images/item.png",
    "images/item.png",
    "images/item.png",
    "images/item.png",
    "images/item.png",
    "images/item.png",
  ];

  // ✅ 要素を動的に生成
  images.forEach((src) => {
    const item = document.createElement("div");
    item.className = "scroll-item";

    const img = document.createElement("img");
    img.src = src;
    img.alt = "サムネイル";

    item.appendChild(img);
    scrollList.appendChild(item);
  });

  // ✅ 要素数に応じてスケール調整（オプション）
  const count = images.length;
  let scale = 1;
  if (count > 6 && count <= 12) scale = 0.9;
  if (count > 12) scale = 0.8;

  scrollList.style.transform = `scale(${scale})`;
  scrollList.style.transformOrigin = "center center";
});
