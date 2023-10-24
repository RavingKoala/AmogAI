namespace AmogAI.SteeringBehaviour;

using System.Drawing.Drawing2D;
using System.Text;

public class Matrix {
    float[,] mat = new float[3, 3];

    public Matrix() {
        mat = Identity().mat;
    }

    public Matrix(float m11, float m12,
                      float m21, float m22) {
        mat[0, 0] = m11; mat[0, 1] = m12;
        mat[1, 0] = m21; mat[1, 1] = m22;
    }

    public Matrix(float m11, float m12, float m13,
                  float m21, float m22, float m23,
                  float m31, float m32, float m33) {
        mat[0, 0] = m11; mat[0, 1] = m12; mat[0, 2] = m13;
        mat[1, 0] = m21; mat[1, 1] = m22; mat[1, 2] = m23;
        mat[2, 0] = m31; mat[2, 1] = m32; mat[2, 2] = m33;
    }

    public Matrix(Vector v) {
        mat[0, 0] = v.X;
        mat[1, 0] = v.Y;
    }

    public Vector ToVector() {
        return new Vector(mat[0, 0], mat[1, 0]);
    }

    public static Matrix operator +(Matrix m1, Matrix m2) {
        Matrix m = new Matrix(m1.mat[0, 0], m1.mat[0, 1], m1.mat[0, 2],
                                m1.mat[1, 0], m1.mat[1, 1], m1.mat[1, 2],
                                m1.mat[2, 0], m1.mat[2, 1], m1.mat[2, 2]);

        for (int r = 0; r < m1.mat.GetLength(0); r++) {
            for (int c = 0; c < m1.mat.GetLength(1); c++) {
                m.mat[r, c] += m2.mat[r, c];
            }
        }

        return m;
    }

    public static Matrix operator -(Matrix m1, Matrix m2) {
        Matrix m = new Matrix(m1.mat[0, 0], m1.mat[0, 1], m1.mat[0, 2],
                                m1.mat[1, 0], m1.mat[1, 1], m1.mat[1, 2],
                                m1.mat[2, 0], m1.mat[2, 1], m1.mat[2, 2]);

        for (int r = 0; r < m1.mat.GetLength(0); r++) {
            for (int c = 0; c < m1.mat.GetLength(1); c++) {
                m.mat[r, c] -= m2.mat[r, c];
            }
        }

        return m;
    }

    public static Matrix operator *(Matrix m1, float f) {
        Matrix m = new Matrix(m1.mat[0, 0], m1.mat[0, 1], m1.mat[0, 2],
                                m1.mat[1, 0], m1.mat[1, 1], m1.mat[1, 2],
                                m1.mat[2, 0], m1.mat[2, 1], m1.mat[2, 2]);

        for (int r = 0; r < m1.mat.GetLength(0); r++) {
            for (int c = 0; c < m1.mat.GetLength(1); c++) {
                m.mat[r, c] *= f;
            }
        }

        return m;
    }

    public static Matrix operator *(float f, Matrix m1) {
        Matrix m = new Matrix(m1.mat[0, 0], m1.mat[0, 1], m1.mat[0, 2],
                                 m1.mat[1, 0], m1.mat[1, 1], m1.mat[1, 2],
                                 m1.mat[2, 0], m1.mat[2, 1], m1.mat[2, 2]);
        return m * f;
    }
        
    public static Matrix operator *(Matrix m1, Matrix m2) {
        Matrix m = new Matrix(m1.mat[0, 0], m1.mat[0, 1], m1.mat[0, 2],
                                m1.mat[1, 0], m1.mat[1, 1], m1.mat[1, 2],
                                m1.mat[2, 0], m1.mat[2, 1], m1.mat[2, 2]);

        for (int r = 0; r < m1.mat.GetLength(0); r++) {
            for (int c = 0; c < m1.mat.GetLength(1); c++) {
                float sum = 0f;

                for (int i = 0; i < m1.mat.GetLength(0); i++) {
                    sum += m1.mat[r, i] * m2.mat[i, c];
                }

                m.mat[r, c] = sum;
            }
        }

        return m;
    }

    public static Vector operator *(Matrix m1, Vector v) {
        return (m1 * new Matrix(v)).ToVector();
    }

    public static Matrix Identity() {
        return new Matrix(1, 0, 0,
                           0, 1, 0,
                           0, 0, 1);
    }

    public static Matrix Scale(float s) {
        return Identity() * s;
    }

    public static Matrix RotateMatrix(float degrees) {
        double rad = degrees * (Math.PI / 180);

        return new Matrix((float)Math.Cos(rad), (float)-Math.Sin(rad),
            (float)Math.Sin(rad), (float)Math.Cos(rad));
    }

    public static Matrix RotateMatrix(Vector heading, Vector side) {
        Matrix m = Identity();
        m.mat[0, 0] = heading.X;
        m.mat[0, 1] = heading.Y;
        m.mat[1, 0] = side.X;
        m.mat[1, 1] = side.Y;

        return m;
    }

    public static Matrix Translate(Vector v) {
        Matrix m = Identity();
        m.mat[2, 0] = v.X;
        m.mat[2, 1] = v.Y;
        return m;
    }

    public void TransformVector(Vector v) {
        //float tempX = (m_Matrix._11 * vPoint.x) + (m_Matrix._21 * vPoint.y) + (m_Matrix._31);
        //float tempY = (m_Matrix._12 * vPoint.x) + (m_Matrix._22 * vPoint.y) + (m_Matrix._32);
        //vPoint.x = tempX;
        //vPoint.y = tempY;

        float tempX = (mat[0, 0] * v.X) + (mat[1, 0] * v.Y) + (mat[2, 0]);
        float tempY = (mat[0, 1] * v.X) + (mat[1, 1] * v.Y) + (mat[2, 1]);

        v.X = tempX;
        v.Y = tempY;
    }
    public override string ToString() {
        StringBuilder sb = new StringBuilder();

        for (int r = 0; r < mat.GetLength(0); r++) {
            for (int c = 0; c < mat.GetLength(1); c++) {
                sb.Append(mat[r, c]);
                sb.Append(' ');
            }
            sb.AppendLine();
        }

        return sb.ToString();
    }
}
