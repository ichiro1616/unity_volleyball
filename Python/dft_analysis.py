import numpy as np
import matplotlib.pyplot as plt

import pandas as pd
import matplotlib.pyplot as plt


file_path = r"C:\Users\isapo\Documents\unity\ServeSpeed_VariedRPM\Assets\Resources\データ収集\241008_Ichito\position_data_rotation_varied0_Ichito.csv"
data = pd.read_csv(file_path)


signal = pd.Series(data.iloc[:, 1][:300])
print(signal)
fs = 1  # サンプリング周波数 (Hz)

# DFTの計算
N = len(signal)  # サンプル数
t = np.arange(N) / fs  # 時間軸を設定 (0からN/fsまで)

dft = np.fft.fft(signal)  # DFT
frequencies = np.fft.fftfreq(N, d=1/fs)  # 周波数軸の生成

# 結果のプロット
plt.figure(figsize=(12, 6))

# 元の信号
plt.subplot(2, 1, 1)
plt.plot(t, signal)
plt.title('Original Signal')
plt.xlabel('trial count')
plt.ylabel('Amplitude')
# plt.yticks(np.arange(-30, 31, 10))  # Y軸の目盛り
# plt.ylim(-30, 30)  # Y軸の範囲設定


plt.subplot(2, 1, 2)
plt.plot(frequencies, np.abs(dft))
plt.title("Frequency Domain Signal (DFT)")
plt.xlabel("Frequency (Hz)")
plt.ylabel("Magnitude")
plt.xlim(0, fs/2)  # ナイキスト周波数まで表示
plt.tight_layout()
plt.yticks(np.arange(0, 2001, 500))  # Y軸の目盛り
plt.ylim(0, 2000)  # Y軸の範囲設定

plt.show()
