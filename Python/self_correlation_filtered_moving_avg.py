import numpy as np
import pandas as pd
import statsmodels.api as sm
import matplotlib.pyplot as plt


file_path = r"C:\Users\isapo\Documents\unity\ServeSpeed_VariedRPM\Assets\Resources\データ収集\241008_Matsuo\position_data_rotation_varied0_Matsuo.csv"
data = pd.read_csv(file_path)



time_series = pd.Series(data.iloc[:, 18][:300]) #pre
print(time_series)


# N = 2 の移動平均を適用
moving_avg_series = time_series.rolling(window=10).mean().dropna()  # 移動平均適用後、NaNを削除
print(len(moving_avg_series))

# 時系列データの自己相関をプロット
fig, ax = plt.subplots(figsize=(10, 6))  # プロットサイズを指定
autocorr = sm.graphics.tsa.plot_acf(moving_avg_series, lags=290, ax=ax,alpha=0.05)  # 自己相関をプロット

# グラフのカスタマイズ
ax.set_title('自己相関係数', fontname="MS Gothic")  # タイトル設定
ax.set_xlabel('ラグ', fontname="MS Gothic")  # X軸ラベル設定
ax.set_ylabel('自己相関係数', fontname="MS Gothic")  # Y軸ラベル設定
# ax.set_xticks(np.arange(0, 201, 10))  # X軸の目盛り設定
ax.axhline(0, color='gray', linestyle='--')  # 0の基準線

plt.grid()
plt.show()
