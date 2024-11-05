import numpy as np
import pandas as pd
import statsmodels.api as sm
import matplotlib.pyplot as plt
from scipy.signal import butter, filtfilt

# データの読み込み
file_path = r"C:\Users\isapo\Documents\unity\ServeSpeed_VariedRPM\Assets\Resources\データ収集\241007_Nao\position_data_rotation_varied0_Nao.csv"
data = pd.read_csv(file_path)

# 時系列データの抽出
time_series = pd.Series(data.iloc[:, 18][:300])  # pre

# ローパスフィルタの設計
def butter_lowpass(cutoff, fs, order=2):
    nyquist = 0.5 * fs
    normal_cutoff = cutoff / nyquist
    b, a = butter(order, normal_cutoff, btype='low', analog=False)
    return b, a

def lowpass_filter(data, cutoff, fs, order=2):
    b, a = butter_lowpass(cutoff, fs, order=order)
    y = filtfilt(b, a, data)
    return y

# パラメータ設定
cutoff_frequency = 0.1  # カットオフ周波数 (Hz)
sampling_frequency = 1.0  # サンプリング周波数 (Hz)

# ローパスフィルタを適用
filtered_data = lowpass_filter(time_series, cutoff_frequency, sampling_frequency)

# フィルタリング後のデータの確認
plt.figure(figsize=(12, 6))
plt.subplot(2, 1, 1)
plt.plot(time_series, label='Original Data')
plt.title('Original Time Series Data')
plt.xlabel('Sample Index')
plt.ylabel('Amplitude')
plt.legend()

plt.subplot(2, 1, 2)
plt.plot(filtered_data, label='Filtered Data', color='orange')
plt.title('Filtered Time Series Data (Low-pass)')
plt.xlabel('Sample Index')
plt.ylabel('Amplitude')
plt.legend()

plt.tight_layout()
plt.show()

# フィルタリング前後の自己相関をプロット
fig, (ax1, ax2) = plt.subplots(2, 1, figsize=(10, 10))  # 2つのサブプロットを作成

# 元データの自己相関をプロット
sm.graphics.tsa.plot_acf(time_series, lags=299, ax=ax1, alpha=0.05)
ax1.set_title('自己相関係数 (元データ)', fontname="MS Gothic")
ax1.set_xlabel('ラグ', fontname="MS Gothic")
ax1.set_ylabel('自己相関係数', fontname="MS Gothic")
ax1.axhline(0, color='gray', linestyle='--')

# フィルタリング後のデータの自己相関をプロット
sm.graphics.tsa.plot_acf(filtered_data, lags=299, ax=ax2, alpha=0.05)
ax2.set_title('自己相関係数 (フィルタ後データ)', fontname="MS Gothic")
ax2.set_xlabel('ラグ', fontname="MS Gothic")
ax2.set_ylabel('自己相関係数', fontname="MS Gothic")
ax2.axhline(0, color='gray', linestyle='--')

plt.tight_layout()
plt.grid()
plt.show()