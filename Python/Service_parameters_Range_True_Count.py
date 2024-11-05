import matplotlib.pyplot as plt
from mpl_toolkits.mplot3d import Axes3D
import pandas as pd

# CSVファイルからデータを読み込む
file_path = r"C:\Users\isapo\Documents\unity\My project (12)\Assets\Resources\position_data_rotation_63.csv"
df = pd.read_csv(file_path)


filtered_df = df[df['judge'] == True]
print(len(df))
for i in range(30, 200, 2):
    filtered_df = df[(df['judge'] == True) & (df['Velocity'] == i)]
    print(f"{i} km/h: {len(filtered_df)}")