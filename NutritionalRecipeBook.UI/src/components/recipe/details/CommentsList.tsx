import { Button, List, Pagination, Popconfirm, Rate } from 'antd';
import { DeleteOutlined } from '@ant-design/icons';
import type { CommentModel } from '@models';
import { useMemo } from 'react';

interface CommentsListProps {
  comments: CommentModel[];
  page: number;
  setPage: (p: number) => void;
  pageSize?: number;
  myCommentIds?: Set<string>;
  deletingId: string | null;
  onDelete: (comment: CommentModel, currentPageCount: number) => Promise<void> | void;
}

export function CommentsList({
  comments,
  page,
  setPage,
  pageSize = 3,
  myCommentIds,
  deletingId,
  onDelete,
}: CommentsListProps) {
  const pagedComments = useMemo(() => {
    const start = (page - 1) * pageSize;
    return comments.slice(start, start + pageSize);
  }, [comments, page, pageSize]);

  console.log(comments);

  return (
    <>
      <List
        dataSource={pagedComments}
        renderItem={(item: CommentModel) => (
          <List.Item className="border-b last:border-0 border-[var(--border)] text-[var(--fg)]">
            <div className="w-full">
              <div className="flex items-center justify-between mb-1">
                <Rate disabled value={item.rating} />
                <span className="text-xs text-[var(--fg-muted)]">{new Date(item.createdAt).toLocaleString()}</span>
              </div>
              {item.id && myCommentIds?.has(item.id) && (
                <div className="flex justify-end mb-2">
                  <Popconfirm
                    title="Are you sure you want to delete this comment?"
                    onConfirm={async (e) => {
                      e?.stopPropagation();
                      await onDelete(item, pagedComments.length);
                    }}
                    okText="Yes"
                    cancelText="No"
                  >
                    <Button
                      type="primary"
                      danger
                      icon={<DeleteOutlined />}
                      loading={deletingId === item.id}
                      onClick={(e) => e.stopPropagation()}
                      aria-label="Delete comment"
                      size="small"
                      className={"!absolute"}
                    />
                  </Popconfirm>
                </div>
              )}
              <div className="whitespace-pre-wrap break-words">{item.content}</div>
            </div>
          </List.Item>
        )}
      />
      <div className="flex justify-end mt-3">
        <Pagination
          current={page}
          pageSize={pageSize}
          total={comments.length}
          size="small"
          onChange={(p) => setPage(p)}
          showSizeChanger={false}
        />
      </div>
    </>
  );
}

export default CommentsList;
