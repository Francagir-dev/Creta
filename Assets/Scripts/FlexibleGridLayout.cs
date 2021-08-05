using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class FlexibleGridLayout : LayoutGroup
{
    [SerializeField] private int rows, columns;
    [SerializeField] private Vector2 cellSize, spacing;
    [SerializeField] private FitType fitType;

    private enum FitType
    {
        Uniform,
        Width,
        Height,
        FixedRows,
        FixedColumns
    }

    [SerializeField] private bool fitX, fitY;

    public override void CalculateLayoutInputHorizontal()
    {
        base.CalculateLayoutInputHorizontal();

        if (fitType.Equals(FitType.Height) || fitType.Equals(FitType.Width) || fitType.Equals(FitType.Uniform))
        {
            CalculateRows();
        }

        switch (fitType)
        {
            case FitType.Uniform:
                CalculateRows();
                break;
            case FitType.Width:
                CalculateRows();
                rows = Mathf.CeilToInt(transform.childCount / (float) columns);
                break;
            case FitType.Height:
                CalculateRows();
                columns = Mathf.CeilToInt(transform.childCount / (float) rows);
                break;
        }

        if (fitType.Equals(FitType.Width) || fitType.Equals(FitType.FixedColumns))
            rows = Mathf.CeilToInt(transform.childCount / (float) columns);

        if (fitType.Equals(FitType.Height) || fitType.Equals(FitType.FixedRows))
            columns = Mathf.CeilToInt(transform.childCount / (float) rows);


        var rect = rectTransform.rect;
        float parentWidth = rect.width;
        float parentHeight = rect.height;

        float cellWidth = (parentWidth / (float) columns) - ((spacing.x / (float) columns) * 2) -
                          (m_Padding.left / (float) columns) - (m_Padding.right / (float) columns);
        float cellHeight = (parentHeight / (float) rows) - ((spacing.y / (float) rows) * 2) -
                           (m_Padding.top / (float) rows) - (m_Padding.bottom / (float) rows);

        cellSize.x = fitX ? cellWidth : cellSize.x;
        cellSize.y = fitY ? cellHeight : cellSize.y;

        int columnCount = 0;
        int rowCount = 0;

        for (int i = 0; i < rectChildren.Count; i++)
        {
            rowCount = i / columns;
            columnCount = i % columns;
            var item = rectChildren[i];

            var xPos = (cellSize.x * columnCount) + (spacing.x * columnCount) + m_Padding.left;
            var yPos = (cellSize.y * rowCount) + (spacing.y * rowCount) + m_Padding.top;
            SetChildAlongAxis(item, 0, xPos, cellSize.x);
            SetChildAlongAxis(item, 1, yPos, cellSize.y);
        }


        switch (fitType)
        {
            case FitType.Width:
                rows = Mathf.CeilToInt(transform.childCount / (float) columns);
                break;
            case FitType.Height:
                columns = Mathf.CeilToInt(transform.childCount / (float) rows);
                break;
        }
    }


    public override void CalculateLayoutInputVertical()
    {
        throw new System.NotImplementedException();
    }

    public override void SetLayoutHorizontal()
    {
        throw new System.NotImplementedException();
    }

    public override void SetLayoutVertical()
    {
        throw new System.NotImplementedException();
    }

    private void CalculateRows()
    {
        fitX = true;
        fitY = true;
        float sqrRt = Mathf.Sqrt(transform.childCount);
        rows = Mathf.CeilToInt(sqrRt);
        columns = Mathf.CeilToInt(sqrRt);
    }
}