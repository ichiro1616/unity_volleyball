import matplotlib.pyplot as plt
from mpl_toolkits.mplot3d import Axes3D
import pandas as pd

# CSVファイルからデータを読み込む
file_path = r"C:\Users\isapo\Documents\unity\ServeSpeed_VariedRPM\Assets\Resources\position_data_rotation_10.5.csv"
df = pd.read_csv(file_path)

# 3Dプロットの作成
fig = plt.figure()
ax = fig.add_subplot(111, projection='3d')

filtered_df = df[df['judge'] == True]
# filtered_df = df
print(len(df))
# データをプロット
ax.scatter(filtered_df['X-Y_Angle'], filtered_df['X-Z_Angle'], filtered_df['Velocity'])
ax.set_zticks([40, 60, 80, 100, 120,140,160,180])
ax.set_zlim(40, 180)
# ラベルの設定
ax.set_zlabel('速度[km/h]', fontname="MS Gothic")
ax.set_xlabel('打ち上げ角[°]', fontname="MS Gothic")
ax.set_ylabel('方位角[°]', fontname="MS Gothic")
ax.set_title('サーブが成功する範囲', fontname="MS Gothic")

# グラフの表示
plt.show()
