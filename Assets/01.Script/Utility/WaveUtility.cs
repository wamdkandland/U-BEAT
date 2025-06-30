using System;
using System.IO;
using UnityEngine;

// �� Ŭ������ Unity���� ���� �����(AudioClip)�� WAV ���Ϸ� �����ϴ� ��ƿ��Ƽ
public static class WaveUtility
{
    const int HEADER_SIZE = 44; // WAV ���� �պκп� ���� ����� ũ��(������: 44����Ʈ)
    /* 
     * Header(���)��? ������ ���� �κп� ��ġ�ϴ� ���� �κ�.
     * �� ������ ������ ����(���� ����� ������)�� �ùٸ��� �а� ����ϱ� ���� �ݵ�� �ʿ�
     * ������� ��������, ����� ����, ä�� ��, ���ø� ���ļ�, ��Ʈ�� ���� ��, ��ü ������ ũ�� ���� ���Ե�
     * ��, 44����Ʈ�� ��� ������ �̸� Ȯ���س��� ���� ��ġ�� 
     * WAV ���� ǥ�� �԰����� 44����Ʈ ����
     */

    // AudioClip�� WAV �������� �����ϴ� �޼ҵ�
    public static void Save(AudioClip clip, string filepath)
    {
        // ���޹��� ������� ���� ��� ����ó��(������ �߻�)
        if (clip == null) throw new ArgumentNullException("clip");

        // ���� ��ΰ� ����ְų� ���� ��� ����ó��(������ �߻�)
        if (string.IsNullOrEmpty(filepath)) throw new ArgumentNullException("filepath");

        // ������ ������ ������ ������ ���� ����
        Directory.CreateDirectory(Path.GetDirectoryName(filepath));

        // �� WAV ������ ���� ����� ����� �����͸� ä���
        using (var fileStream = CreateEmpty(filepath))
        {
            ConvertAndWrite(fileStream, clip); // ���� ����� �����͸� ���Ͽ� ����
            WriteHeader(fileStream, clip); // WAV ������ ����� �ۼ�
        }

        /*
         * using Ű������ ����
         * 1. using System.IO; ó�� ���̺귯���� ������ ����� �� ���
         * 2. �� ó�� �ڿ��� �����ϰ� �����ϱ� ���� ���
         * using ��� �ȿ� ����� ����(�� fileStream)�� �����̳� ��Ʈ��ũ �� �ڿ��� ����ϴ� ��ü��
         * �ش� �ڿ��� ����� �Ŀ��� �ݵ�� �ݾ��ְų� �޸𸮿��� �������ִ� ������ �ʿ��ϴ�.
         * �̷� �� �ڵ����� �ڿ��� �������ִ� ����� using Ű������ �� ��° �뵵
         */

        /*
         * using Ű���� ��ü ����
         * var fileStream = CreateEmpty(filepath);
         * ConvertAndWrite(fileStream, clip);
         * WriteHeader(fileStream, clip);
         * fileStream.Dispose(); // �ݵ�� ȣ���ؾ� ������ �ر� ����
         */
    }

    // WAV ������ ���� �� ���·� ����
    private static FileStream CreateEmpty(string filepath)
    {
        var fileStream = new FileStream(filepath, FileMode.Create); // ������ ����
        byte emptyByte = new byte(); // �� ����Ʈ ��(0)

        // WAV ���� ����� ���� ������ �̸� 0���� ä������
        for (int i = 0; i < HEADER_SIZE; i++)
        {
            fileStream.WriteByte(emptyByte);
        }

        return fileStream; // ���� �� ������ �����ֱ�
    }

    // Unity ����� �����͸� WAV ������ �����ͷ� ��ȯ�Ͽ� ���Ͽ� ���
    private static void ConvertAndWrite(FileStream fileStream, AudioClip clip)
    {
        float[] samples = new float[clip.samples]; // ����� �����͸� ������ �迭
        clip.GetData(samples, 0); // AudioClip���� �����͸� �迭�� �����ɴϴ�.

        Int16[] intData = new Int16[samples.Length]; // PCM 16��Ʈ ���� �����͸� ���� �迭
        Byte[] bytesData = new Byte[samples.Length * 2]; // 16��Ʈ�� 2����Ʈ�̹Ƿ� ũ�� ����

        const float rescaleFactor = 32767; // float ���� ������ ��ȯ�� �� ����ϴ� ������

        // ����� �����͸� �ϳ��� ó���ؼ� WAV ���� ���Ŀ� �´� ����Ʈ�� ��ȯ
        for (int i = 0; i < samples.Length; i++)
        {
            intData[i] = (short)(samples[i] * rescaleFactor); // float���� 16��Ʈ ������ ��ȯ
            byte[] byteArr = BitConverter.GetBytes(intData[i]); // ���� �����͸� ����Ʈ�� ��ȯ
            byteArr.CopyTo(bytesData, i * 2); // ����Ʈ �迭�� ����
        }

        // ����Ʈ �迭�� ��ȯ�� �����͸� ���Ͽ� ����
        fileStream.Write(bytesData, 0, bytesData.Length);
    }

    // WAV ������ �պκ�(���)�� �ʿ��� ������ �ۼ�
    private static void WriteHeader(FileStream fileStream, AudioClip clip)
    {
        var hz = clip.frequency; // ������� ���ø� �ӵ�(�ʴ� ���� ����)
        var channels = clip.channels; // ����� ä�� ��(��: 1�� ���, 2�� ���׷���)
        var samples = clip.samples; // ��ü ����� ������ ������ ����

        fileStream.Seek(0, SeekOrigin.Begin); // ������ ó������ �̵�

        // WAV ������ �����ϴ� ��� ������ ���ʷ� �ۼ�
        // ���� ����(RIFF)
        byte[] riff = System.Text.Encoding.UTF8.GetBytes("RIFF");
        fileStream.Write(riff, 0, 4);

        // ���� ũ�� 
        byte[] chunkSize = BitConverter.GetBytes(fileStream.Length - 8);
        fileStream.Write(chunkSize, 0, 4);

        // WAV �������� ǥ��
        byte[] wave = System.Text.Encoding.UTF8.GetBytes("WAVE");
        fileStream.Write(wave, 0, 4);

        // ���� �κ� ����
        byte[] fmt = System.Text.Encoding.UTF8.GetBytes("fmt ");
        fileStream.Write(fmt, 0, 4);

        // PCM ���� ũ��(�׻� 16)
        byte[] subChunk1 = BitConverter.GetBytes(16);
        fileStream.Write(subChunk1, 0, 4);

        ushort audioFormat = 1;
        ushort numChannels = (ushort)channels;
        byte[] audioFormatBytes = BitConverter.GetBytes(audioFormat);
        byte[] numChannelsBytes = BitConverter.GetBytes(numChannels);
        fileStream.Write(audioFormatBytes, 0, 2); // ����� ����(PCM)
        fileStream.Write(numChannelsBytes, 0, 2); // ä�� ��

        // ���ø� �ӵ�
        byte[] sampleRate = BitConverter.GetBytes(hz);
        fileStream.Write(sampleRate, 0, 4);

        // �ʴ� ������ ũ��
        byte[] byteRate = BitConverter.GetBytes(hz * channels * 2);
        fileStream.Write(byteRate, 0, 4);

        // ��� ũ��
        ushort blockAlign = (ushort)(channels * 2);
        fileStream.Write(BitConverter.GetBytes(blockAlign), 0, 2);

        // ��Ʈ ��(16��Ʈ)
        ushort bitsPerSample = 16;
        fileStream.Write(BitConverter.GetBytes(bitsPerSample), 0, 2);

        // ������ �κ� ����
        byte[] dataString = System.Text.Encoding.UTF8.GetBytes("data");
        fileStream.Write(dataString, 0, 4);

        // ������ ��ü ����
        byte[] subChunk2 = BitConverter.GetBytes(samples * channels * 2);
        fileStream.Write(subChunk2, 0, 4);
    }
}