import pandas as pd
from datetime import datetime
# CSVファイルを読み込み
# input_file = r"C:\Users\isapo\Documents\unity\ServeSpeed_VariedRPM\Assets\Resources\position_data_rotation_varied.csv"  # 読み込むCSVファイル名
input_file = r"C:\Users\isapo\Documents\unity\ServeSpeed_VariedRPM\Assets\Resources\position_data_rotation_varied_add_11.csv"  # 読み込むCSVファイル名


for i in range(21):
    # output_file = r"C:\Users\isapo\Documents\unity\My project (12)\Assets\Resources\random_rotation_val1_leq30_val2_between_neg10_10_val3_True\position_data_rotation_random_state" + str(i) + ".csv"  # 保存するCSVファイル名
    output_file = r"C:\Users\isapo\Documents\unity\ServeSpeed_VariedRPM\Assets\Resources\random_rotation_varied_add_11\position_data_rotation_varied_add_11_" + str(i) + ".csv"  # 保存するCSVファイル名

    # データを読み込む
    data = pd.read_csv(input_file)
    filtered_data = data[
    (data.iloc[:, 0].astype(float) >= 40) &  
    (data.iloc[:, 0].astype(float) <= 80) &  
    (data.iloc[:, 1].astype(float) <= 30) &  
    (data.iloc[:, 2].astype(float).between(-10, 10)) & # values[2]が-10以上、10以下
    (data.iloc[:, 3] == True)  # values[1]が45以下

]

    # 行をランダムに並び替え
    shuffled_data = filtered_data.sample(frac=1, random_state=i).reset_index(drop=True)

    # シャッフルされたデータを新しいCSVファイルに保存
    shuffled_data.to_csv(output_file, index=False)

    print(f"Shuffled CSV saved as: {output_file}")
