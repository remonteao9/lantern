using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using DG.Tweening;

public class MNUfo : MNActor
{
    public override void Awake() {
        base.Awake();
        MNActor[] objects = FindObjectsOfType<MNActor>();
        var others = objects.Where(a => a.gameObject != this.gameObject).ToArray();
        var oth = others[Random.Range(0, others.Length)];
        transform.position = oth.transform.position + Vector3.up * 3;
        oth.transform.SetParent(transform);
        oth.transform.DOScale(Vector3.zero, 1.9f);
        oth.transform.DOMove(transform.position, 2f).OnComplete(() => {
            Destroy(oth.gameObject);
            Destroy(gameObject);
        });
    }
}
