using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour {

	[SerializeField]
	private int rows;
	[SerializeField]
	private int cols;
	[SerializeField]
	private Vector2 gridSize;
	[SerializeField]
	private Vector2 gridOffset;

    [SerializeField]
    private Sprite cellSprite;
    private Vector2 cellSize;
    private Vector2 cellScale;

    void Start () {
        InitCells();
    }
	
	// Update is called once per frame
	void Update () {
    
    }

    void InitCells() {
        GameObject cellObject = new GameObject();

        // cria objeto vazio e adiciona um componente de rederização de sprite, coloca o sprite em cellSprite
        cellObject.AddComponent<SpriteRenderer>().sprite = cellSprite;

        // pega tamanho sprite
        cellSize = cellSprite.bounds.size;

        // pega novo tamanho de celula e ajusta ela para para ficar dentro na grid
        Vector2 newCellSize = new Vector2(gridSize.x / (float)cols, gridSize.y / (float)rows);

        // escala celulas para ficarem na grid
        cellScale.x = newCellSize.x / cellSize.x;
        cellScale.y = newCellSize.y / cellSize.y;

        // antigo tamanho troca com novo
        cellSize = newCellSize;

        cellObject.transform.localScale = new Vector2(cellScale.x, cellScale.y);

        // posiciona as celulas entro da grid
        gridOffset.x = -(gridSize.x / 2) + (cellSize.x / 2);
        gridOffset.y = -(gridSize.y / 2) + (cellSize.y / 2);

        // preenche grid com celulas
        for (int row = 0; row < rows; row++){
            for (int col = 0; col < cols; col++){
                // adiciona celulas com posições diferentes
                Vector2 pos = new Vector2(col * cellSize.x + gridOffset.x + transform.position.x, row * cellSize.y + gridOffset.y + transform.position.y);

                // instancia posição
                GameObject c0 = Instantiate(cellObject, pos, Quaternion.identity) as GameObject;

                // Grid é setada como parente para que seu tamanho seja alterado junto com as celulas
                c0.transform.parent = transform;
            }
        }

        // detroi o objeto usado para instanciar
        Destroy(cellObject);
    }
    void OnDrawGizmos() {
        Gizmos.DrawWireCube(transform.position, gridSize);
    }
}
