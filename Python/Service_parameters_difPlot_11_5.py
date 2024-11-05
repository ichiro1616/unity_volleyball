import pandas as pd
import numpy as np
import matplotlib.pyplot as plt

from matplotlib import font_manager

# 日本語フォントを設定（MS Gothicがインストールされている場合）
plt.rcParams['font.family'] = 'MS Gothic'  # 'MS Gothic'がない場合は他の日本語フォントを指定

# CSVファイルを読み込む
file_path = r"C:\Users\isapo\Documents\unity\ServeSpeed_VariedRPM\Assets\Resources\データ収集\241011_Nao\python_output_Nao_11_5.csv"
data = pd.read_csv(file_path)

x1 = data.iloc[:, 0][:14]
y1 = data.iloc[:, 2][:14]

x2 = data.iloc[:, 3][:61]
y2 = data.iloc[:, 5][:61]

x3 = data.iloc[:, 6][:15]
y3 = data.iloc[:, 8][:15]

x4 = data.iloc[:, 9][:10]
y4 = data.iloc[:, 11][:10]

# 近似曲線を計算
coef1 = np.polyfit(x1, y1, 1)
coef2 = np.polyfit(x2, y2, 1)
coef3 = np.polyfit(x3, y3, 1)
coef4 = np.polyfit(x4, y4, 1)

# 近似式を使ってy値を計算
poly1 = np.poly1d(coef1)
poly2 = np.poly1d(coef2)
poly3 = np.poly1d(coef3)
poly4 = np.poly1d(coef4)


# x = 40 から 80 までの値を生成
x_range = np.linspace(40, 80, 100)  # 40から80の範囲で100点生成

# 各近似式にx_rangeを適用
y_range1 = poly1(x_range)
y_range2 = poly2(x_range)
y_range3 = poly3(x_range)
y_range4 = poly4(x_range)


# グラフを作成
fig = plt.figure(figsize=(25, 5))

# 1つ目のグラフ
ax1 = fig.add_subplot(151)
ax1.scatter(x1, y1, marker='o', color='g',label='データポイント')
ax1.plot(x_range, y_range1, color='green', label=f'近似: y={coef1[0]:.3f}x + {coef1[1]:.3f}')
ax1.set_title('0[rpm]')
ax1.set_xlabel('実際の速度[km/h]')
ax1.set_ylabel('速度誤差率[%]')
ax1.set_yticks([-50, -40, -30, -20, -10, 0, 10, 20, 30,40,50])
ax1.set_ylim(-50, 50)
ax1.set_xticks([40, 45, 50, 55, 60, 65, 70, 75, 80])
ax1.set_xlim(40, 80)
ax1.grid(True)
ax1.legend()

# 2つ目のグラフ
ax2 = fig.add_subplot(152)
ax2.scatter(x2, y2, marker='o', color='k', label='データポイント')
ax2.plot(x_range, y_range2, color='black', label=f'近似: y={coef2[0]:.3f}x + {coef2[1]:.3f}')
ax2.set_title('100[rpm]')
ax2.set_xlabel('実際の速度[km/h]')
ax2.set_ylabel('速度誤差率[%]')
ax2.set_yticks([-50, -40, -30, -20, -10, 0, 10, 20, 30,40,50])
ax2.set_ylim(-50, 50)
ax2.set_xticks([40, 45, 50, 55, 60, 65, 70, 75, 80])
ax2.set_xlim(40, 80)
ax2.grid(True)
ax2.legend()

# 3つ目のグラフ
ax3 = fig.add_subplot(153)
ax3.scatter(x3, y3, marker='o', color='b', label='データポイント')
ax3.plot(x_range, y_range3, color='blue', label=f'近似: y={coef3[0]:.3f}x + {coef3[1]:.3f}')
ax3.set_title('300[rpm]')
ax3.set_xlabel('実際の速度[km/h]')
ax3.set_ylabel('速度誤差率[%]')
ax3.set_yticks([-50, -40, -30, -20, -10, 0, 10, 20, 30,40,50])

ax3.set_ylim(-50, 50)
ax3.set_xticks([40, 45, 50, 55, 60, 65, 70, 75, 80])
ax3.set_xlim(40, 80)
ax3.grid(True)
ax3.legend()

# 4つ目のグラフ
ax4 = fig.add_subplot(154)
ax4.scatter(x4, y4, marker='o', color='r', label='データポイント')
ax4.plot(x_range, y_range4, color='red', label=f'近似: y={coef4[0]:.3f}x + {coef4[1]:.3f}')
ax4.set_title('600[rpm]')
ax4.set_xlabel('実際の速度[km/h]')
ax4.set_ylabel('速度誤差率[%]')
ax4.set_yticks([-50, -40, -30, -20, -10, 0, 10, 20, 30,40,50])

ax4.set_ylim(-50, 50)
ax4.set_xticks([40, 45, 50, 55, 60, 65, 70, 75, 80])
ax4.set_xlim(40, 80)
ax4.grid(True)
ax4.legend()

# 4つ目のグラフ: 3つの近似直線を重ねたもの
ax5 = fig.add_subplot(155)
ax5.plot(x_range, y_range1, color='green', label='0[rpm] 近似')
ax5.plot(x_range, y_range2, color='black', label='100[rpm] 近似')
ax5.plot(x_range, y_range3, color='blue', label='300[rpm] 近似')
ax5.plot(x_range, y_range4, color='red', label='600[rpm] 近似')

ax5.set_title('比較: 4つの近似直線')
ax5.set_xlabel('実際の速度[km/h]')
ax5.set_ylabel('速度誤差率[%]')

ax5.set_yticks([-50, -40, -30, -20, -10, 0, 10, 20, 30,40,50])

ax5.set_ylim(-50, 50)
ax5.set_xticks([40, 45, 50, 55, 60, 65, 70, 75, 80])
ax5.set_xlim(40, 80)
ax5.grid(True)
ax5.legend()

# グラフを表示
plt.tight_layout()
plt.show()
