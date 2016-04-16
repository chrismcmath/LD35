using System;
using System.Collections.Generic;

using UnityEngine;
using Vectrosity;

namespace Rf.Utils {
    public class LineUtils {
        public static void CheckLine(ref VectorLine line, bool enabled, int points,
            Material mat, float lineWidth, float textureScale=0.0f) {
            if (enabled && points > 1) {
                if (line == null) {
                    line = new VectorLine("VectorLine", new Vector3[points],
                        mat, lineWidth, LineType.Continuous, Joins.Weld);
                    line.SetTextureScale(1.0f);
                } else {
                    line.lineWidth = lineWidth;
                    line.material = mat;
                    line.SetTextureScale(textureScale);
                }
            } else {
                if (line != null) {
                    VectorLine.Destroy(ref line);
                }
            }
        }

        public static void CheckPoints(ref VectorPoints points, bool enabled, int num, Material mat, float size) {
            if (enabled) {
                if (points == null) {
                    points = new VectorPoints("VectorLine", new Vector3[num],
                        mat, size);
                } else {
                    points.lineWidth = size;
                    points.material = mat;
                }
            } else {
                if (points != null) {
                    VectorPoints.Destroy(ref points);
                }
            }
        }

        public static void ClearLine(VectorLine line) {
            if (line == null) return;
            line.ZeroPoints();
            line.drawEnd = 0;
            line.Draw();
        }

        public static void ClearPoints(VectorPoints points) {
            if (points == null) return;
            points.ZeroPoints();
            points.drawEnd = 0;
            points.Draw();
        }
    }
}
