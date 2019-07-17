using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using System;

public class mapping : MonoBehaviour
{

    public int maxHeight = 5;
    public int maxWidth = 9;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI scoreDead;
    public TextMeshProUGUI bestScoreDead;
    public GameObject gameCanvas;
    public GameObject gameOverCanvas;
    public GameObject upButton;
    public GameObject downButton;
    public GameObject leftButton;
    public GameObject rightButton;
    public GameObject gameWin;
    public GameObject wall;
    int score = 0;
    int bestScore;
    public Color color1;
    public Color color2;
    Color playerColor;
    public Color heartColor = Color.red;
    GameObject playerObj;
    GameObject mapObject;
    GameObject heartObj;
    GameObject tailParent;
    SpriteRenderer mapRenderer;
    Sprite playerSprite;
    Node[,] grid;
    Node playerNode;
    Node heartNode;
    Node prevPlayerNode;
    Direction currentDir;

    float timer;
    public float moveRate = 0.25f;
    List<Node> availableNodes = new List<Node>();
    List<SpecialNodes> tail = new List<SpecialNodes>();
    public enum Direction
    {
        up, down, left, right
    }

    void Start()
    {
        //color1 = new Color(UnityEngine.Random.Range(0, 255) / 255f, UnityEngine.Random.Range(0, 255) / 255f, UnityEngine.Random.Range(0, 255) / 255f);
        //color2 = new Color(UnityEngine.Random.Range(0, 255) / 255f, UnityEngine.Random.Range(0, 255) / 255f, UnityEngine.Random.Range(0, 255) / 255f);
        //color1 = Color.black;
        //color2 = Color.yellow;
        //color2 = color1;
        //color2.g -= .2f;
        //color2.b -= .3f;
        playerColor = gameManager.color;
        gameCanvas.SetActive(true);
        scoreText.text = score.ToString();
        createMap();
        placePlayer();
        CreateHeart();
        currentDir = Direction.up;
        upButton.SetActive(false);
        downButton.SetActive(false);
        leftButton.SetActive(true);
        rightButton.SetActive(true);
        gameOverCanvas.SetActive(false);
        gameWin.SetActive(false);
        wall.SetActive(true);
    }

    bool up, down, left, right;

    void GetButton()
    {
        up = Input.GetButtonDown("Up");
        down = Input.GetButtonDown("Down");
        left = Input.GetButtonDown("Left");
        right = Input.GetButtonDown("Right");
    }

    void Update()
    {
        GetButton();
        SetPlayerDirection();
        timer += Time.deltaTime;
        if (timer > moveRate)
        {
            timer = 0;
            MovePlayer();
        }

    }

    void CreateHeart()
    {
        heartObj = new GameObject("Heart");
        SpriteRenderer heartRenderer = heartObj.AddComponent<SpriteRenderer>();
        heartRenderer.sprite = CreateSprite(heartColor);
        heartRenderer.sortingOrder = 1;
        RandomlyPlaceHeart();
    }

    void RandomlyPlaceHeart()
    {
        int rand = UnityEngine.Random.Range(0, availableNodes.Count);
        Node n = availableNodes[rand];
        while (IsTailNode(n))
        {
            rand = UnityEngine.Random.Range(0, availableNodes.Count);
            n = availableNodes[rand];
        }
        PlacePlayerObj(heartObj, n.worldPosition);
        //heartObj.transform.position = n.worldPosition;
        heartNode = n;
    }

    void SetPlayerDirection()
    {
        if (up)
        {
            SetDirection(Direction.up);

        }
        else if (down)
        {
            SetDirection(Direction.down);

        }
        else if (left)
        {
            SetDirection(Direction.left);

        }
        else if (right)
        {
            SetDirection(Direction.right);

        }
    }

    void MovePlayer()
    {
        int x = 0;
        int y = 0;
        switch (currentDir)
        {
            case Direction.up:
                y = 1;
                break;
            case Direction.down:
                y = -1;
                break;
            case Direction.left:
                x = -1;
                break;
            case Direction.right:
                x = 1;
                break;
        }
        Node targetNode = GetNode(playerNode.x + x, playerNode.y + y);
        if (targetNode == null)
        {
            Time.timeScale = 0f;
            gameCanvas.SetActive(false);
            gameOverCanvas.SetActive(true);
            scoreDead.text = "Score: " + score.ToString();
            bestScoreDead.text = "Highscore: " + PlayerPrefs.GetInt("bestscore").ToString();
            //SceneManager.LoadScene("Snake");
        }
        else
        {
            if (IsTailNode(targetNode))
            {
                //game over
                //SceneManager.LoadScene("Snake");
                Time.timeScale = 0f;
                gameCanvas.SetActive(false);
                gameOverCanvas.SetActive(true);
                scoreDead.text = "Score: " + score.ToString();
                bestScoreDead.text = "Highscore: " + PlayerPrefs.GetInt("bestscore").ToString();
            }
            else
            {
                bool isScore = false;

                if (targetNode == heartNode)
                {
                    isScore = true;
                }
                Node previousNode = playerNode;
                availableNodes.Add(previousNode);



                if (isScore)
                {
                    tail.Add(CreateTailNode(previousNode.x, previousNode.y));
                    availableNodes.Remove(previousNode);
                }
                MoveTail();
                PlacePlayerObj(playerObj, targetNode.worldPosition);
                //playerObj.transform.position = targetNode.worldPosition;
                playerNode = targetNode;
                availableNodes.Remove(playerNode);
                if (isScore)
                {
                    if (availableNodes.Count > 0)
                    {
                        RandomlyPlaceHeart();
                        score++;
                        scoreText.text = score.ToString();
                        if(score%50==0)
                        {
                            moveRate -= .01f;
                        }
                        if (score > PlayerPrefs.GetInt("bestscore"))
                        {
                            PlayerPrefs.SetInt("bestscore", score);
                        }
                    }
                    else
                    {
                        Time.timeScale = 0f;
                        gameCanvas.SetActive(false);
                        gameWin.SetActive(true);
                    }
                }
            }
        }
    }

    void SetDirection(Direction d)
    {
        if (!IsOpposite(d))
        {
            currentDir = d;
            timer = moveRate + .1f;
        }
    }

    public void createMap()
    {

        mapObject = new GameObject("Map");
        mapRenderer = mapObject.AddComponent<SpriteRenderer>();
        Texture2D txt = new Texture2D(maxWidth, maxHeight);
        grid = new Node[maxWidth, maxHeight];

        for (int x = 0; x < maxWidth; x++)
        {
            for (int y = 0; y < maxHeight; y++)
            {
                Vector3 tp = Vector3.zero;
                tp.x = x;
                tp.y = y;
                Node n = new Node()
                {
                    x = x,
                    y = y,
                    worldPosition = tp
                };
                grid[x, y] = n;
                availableNodes.Add(n);
                if (x % 2 != 0)
                {
                    if (y % 2 != 0)
                    {
                        txt.SetPixel(x, y, color1);
                    }
                    else
                        txt.SetPixel(x, y, color2);
                }
                else
                    if (y % 2 != 0)
                {
                    txt.SetPixel(x, y, color2);
                }
                else
                    txt.SetPixel(x, y, color1);
            }

        }
        txt.filterMode = FilterMode.Point;
        txt.Apply();
        Rect rect = new Rect(0, 0, maxWidth, maxHeight);
        Sprite sprite = Sprite.Create(txt, rect, Vector2.zero, 1f, 0, SpriteMeshType.FullRect);
        mapRenderer.sprite = sprite;
    }

    void placePlayer()
    {
        playerObj = new GameObject("Player");
        SpriteRenderer playerRenderer = playerObj.AddComponent<SpriteRenderer>();
        playerSprite = CreateSprite(playerColor);
        playerRenderer.sprite = playerSprite;
        playerRenderer.sortingOrder = 1;
        playerNode = GetNode(6, 2);

        playerObj.transform.localScale = Vector3.one;
        playerObj.transform.position = playerNode.worldPosition;

        //  PlacePlayerObj(playerObj, playerNode.worldPosition);
        tailParent = new GameObject("tailParent");
    }

    void MoveTail()
    {
        Node previousNode = null;
        for (int i = 0; i < tail.Count; i++)
        {
            SpecialNodes p = tail[i];
            availableNodes.Add(p.node);
            if (i == 0)
            {
                previousNode = p.node;
                p.node = playerNode;
            }
            else
            {
                Node prev = p.node;
                p.node = previousNode;
                previousNode = prev;
            }
            availableNodes.Remove(p.node);
            playerObj.transform.position = p.node.worldPosition;
            PlacePlayerObj(p.obj, p.node.worldPosition);

        }
    }

    bool IsOpposite(Direction d)
    {
        switch (d)
        {
            default:
            case Direction.up:
                if (currentDir == Direction.down)
                    return true;
                else
                    return false;

            case Direction.down:
                if (currentDir == Direction.up)
                    return true;
                else
                    return false;
            case Direction.left:
                if (currentDir == Direction.right)
                    return true;
                else
                    return false;
            case Direction.right:
                if (currentDir == Direction.left)
                    return true;
                else
                    return false;
        }
    }

    bool IsTailNode(Node n)
    {
        for (int i = 0; i < tail.Count; i++)
        {
            if (tail[i].node == n)
            {
                return true;
            }
        }
        return false;
    }

    Node GetNode(int x, int y)
    {
        if (x < 0 || x > maxWidth - 1 || y < 0 || y > maxHeight - 1)
            return null;

        return grid[x, y];
    }

    void PlacePlayerObj(GameObject obj, Vector3 pos)
    {
        pos += Vector3.zero;
        obj.transform.position = pos;
    }

    SpecialNodes CreateTailNode(int x, int y)
    {
        SpecialNodes s = new SpecialNodes();
        s.node = GetNode(x, y);
        s.obj = new GameObject();


        //s.obj.transform.position = s.node.worldPosition;
        s.obj.transform.parent = tailParent.transform;
        s.obj.transform.localScale = Vector3.one;
        PlacePlayerObj(s.obj, s.node.worldPosition);
        SpriteRenderer r = s.obj.AddComponent<SpriteRenderer>();
        r.sprite = playerSprite;
        r.sortingOrder = 1;
        return s;
    }


    Sprite CreateSprite(Color targetColor)
    {
        Texture2D txt = new Texture2D(1, 1);
        txt.SetPixel(0, 0, targetColor);
        txt.Apply();
        txt.filterMode = FilterMode.Point;
        Rect rect = new Rect(0, 0, 1, 1);
        return Sprite.Create(txt, rect, Vector2.zero, 1f, 0, SpriteMeshType.FullRect);
    }

    public void Menu()
    {
        SceneManager.LoadScene("SampleScene");
    }

    public void Replay()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Snake");
    }

    public void ButtonUp()
    {
        SetDirection(Direction.up);
        upButton.SetActive(false);
        downButton.SetActive(false);
        leftButton.SetActive(true);
        rightButton.SetActive(true);
    }

    public void ButtonDown()
    {
        SetDirection(Direction.down);
        upButton.SetActive(false);
        downButton.SetActive(false);
        leftButton.SetActive(true);
        rightButton.SetActive(true);
    }

    public void ButtonLeft()
    {
        SetDirection(Direction.left);
        upButton.SetActive(true);
        downButton.SetActive(true);
        leftButton.SetActive(false);
        rightButton.SetActive(false);
    }

    public void ButtonRight()
    {
        SetDirection(Direction.right);
        upButton.SetActive(true);
        downButton.SetActive(true);
        leftButton.SetActive(false);
        rightButton.SetActive(false);
    }
    public void Back()
    {
        gameManager.ok = true;
        SceneManager.LoadScene("SampleScene");
    }
}