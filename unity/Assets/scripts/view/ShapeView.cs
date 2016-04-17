using UnityEngine;
using System;
using System.Collections;

using Vectrosity;

using Rf.Controls;
using Rf.Core;
using Rf.Models;

namespace Rf.View {
    public class ShapeView : MonoBehaviour {
        public static Action OnNewShape;

        private LineModel _LineModel;
        private MeshFilter _MeshFilter;

        public bool flipNormals = false;

        public void Start() {
            _MeshFilter= gameObject.AddComponent<MeshFilter>();
            _LineModel = Global.Instance.LineModel;

            LineModel.OnLineFinalized += OnLineFinalized;
            Global.OnNewGame += OnNewGame;
        }

        private void OnLineFinalized() {
            Vector2[] points = _LineModel.GetWorldPoints();
            if (points.Length > 2) {
                CreateShape(points);
            }
        }

        private void OnNewGame(string level) {
            _MeshFilter.mesh = null;
        }

        private void CreateShape(Vector2[] points) {
            Mesh mesh = new Mesh();
            _MeshFilter.mesh = mesh;
            mesh.vertices = ConvertArray(points);
            mesh.triangles = GetMeshTriangles(points);
            mesh.uv = GetMeshUVs();

            if (OnNewShape != null) {
                OnNewShape();
            }
        }

        private int[] GetMeshTriangles(Vector2[] points) {
            Triangulator triangulator = new Triangulator(points);

            //TODO: hack, trying to reuse me cast
            PolygonCollider2D col = GetComponent<PolygonCollider2D>();
            if (col != null) {
                Destroy(col);
            }
            col = gameObject.AddComponent<PolygonCollider2D>();
            col.SetPath(0, points);

            return triangulator.Triangulate();
        }

        private Vector2[] GetMeshUVs() {
            return new Vector2[0];
        }

        private Vector3[] ConvertArray(Vector2[] v2){
            Vector3 [] v3 = new Vector3[v2.Length];
            for(int i = 0; i <  v2.Length; i++){
                Vector2 tempV2 = v2[i];
                v3[i] = new Vector3(tempV2.x, tempV2.y);
            }
            return v3;
        }
    }
}
