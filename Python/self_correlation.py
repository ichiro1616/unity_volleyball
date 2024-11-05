import numpy as np
import pandas as pd
import statsmodels.api as sm
import matplotlib.pyplot as plt


file_path = r"C:\Users\isapo\Documents\unity\ServeSpeed_VariedRPM\Assets\Resources\データ収集\241007_Kento\position_data_rotation_varied0_Kento.csv"
data = pd.read_csv(file_path)



time_series = pd.Series(data.iloc[:, 18][:300]) #pre


print(time_series)

# # pandasを使用して自己相関係数を計算
# lagged_series = time_series.shift(1)  # 1つ前のデータをシフト
# correlation = time_series.corr(lagged_series)  # 自己相関係数の計算
# print("自己相関係数 (pandas):", correlation)

# statsmodelsを使用して自己相関係数を計算
# autocorr = sm.tsa.stattools.acf(time_series, nlags=200,)  # 最初の10ラグまで計算
# print("自己相関係数 (statsmodels):", autocorr)
# ラグ101から200までの自己相関を取得
# autocorr_101_200 = autocorr[101:201]
# data_list = [float(x) for x in str(autocorr)[20:-1].split()]
# print("!!!!!!!!!!!!!!!!!!!!!!!!",data_list.index(max(data_list)))

# # 確認用に出力
# print(data_list)
# 時系列データの自己相関をプロット
fig, ax = plt.subplots(figsize=(10, 6))  # プロットサイズを指定
autocorr = sm.graphics.tsa.plot_acf(time_series, lags=299, ax=ax,alpha=0.05)  # 自己相関をプロット

# グラフのカスタマイズ
ax.set_title('自己相関係数', fontname="MS Gothic")  # タイトル設定
ax.set_xlabel('ラグ', fontname="MS Gothic")  # X軸ラベル設定
ax.set_ylabel('自己相関係数', fontname="MS Gothic")  # Y軸ラベル設定
# ax.set_xticks(np.arange(0, 201, 10))  # X軸の目盛り設定
ax.axhline(0, color='gray', linestyle='--')  # 0の基準線

plt.grid()
plt.show()
